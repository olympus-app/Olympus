using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.OData.Results;

namespace Olympus.Api.Controllers;

public class ControllersResultFilter(IOptions<JsonOptions> jsonOptions) : IResultFilter {

	private readonly JsonSerializerOptions SerializerOptions = jsonOptions.Value.JsonSerializerOptions;

	public void OnResultExecuting(ResultExecutingContext context) {

		if (context.Result is not IStatusCodeActionResult statusCodeResult || statusCodeResult.StatusCode is null || statusCodeResult.StatusCode < 400) return;

		if (context.Result is ObjectResult objResult && objResult.Value is ErrorResult) return;

		var httpStatusCode = statusCodeResult.StatusCode is null ? HttpStatusCode.InternalServerError : (HttpStatusCode)statusCodeResult.StatusCode;

		var response = new ErrorResult {
			Status = httpStatusCode.Value,
			Message = httpStatusCode.Humanize() + ".",
			Details = ExtractErrorDetails(context.Result)
		};

		context.Result = new ContentResult {
			Content = JsonSerializer.Serialize(response, SerializerOptions),
			ContentType = ContentTypes.Json,
			StatusCode = response.Status
		};

	}

	public void OnResultExecuted(ResultExecutedContext context) { }

	private static string? ExtractErrorDetails(IActionResult result) {

		return result switch {
			ObjectResult { Value: string message } => message,
			ObjectResult { Value: IDictionary<string, object> dict } when dict.TryGetValue("type", out var type) && type is string strType && strType == "Microsoft.OData.ODataException" => AppErrors.Values.InvalidODataQuery,
			IODataErrorResult odataResult => odataResult.Error.Message,
			_ => null
		};

	}

}
