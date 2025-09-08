using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.OData;

namespace Olympus.Api;

public class ODataErrorHandler() : ODataErrorSerializer {

	public override async Task WriteObjectAsync(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext) {

		var httpContext = writeContext.Request.HttpContext;
		var response = new GlobalErrorResponse() { Error = (int)HttpStatusCode.BadRequest };

		if (graph is ODataError odataError) {

			response.Message = odataError.Message;

		} else if (graph is ODataException odataException) {

			response.Message = odataException.Message;
			response.Details = odataException.InnerException?.Message;

		} else if (graph is SerializableError serializableError) {

			serializableError.TryGetValue("message", out var message);
			serializableError.TryGetValue("ExceptionMessage", out var innerMessage);

			var reason = (string?)message;
			var details = (string?)innerMessage;

			response.Message = reason ?? "Invalid OData query.";
			response.Details = details;

		} else {

			response.Message = "Invalid OData query.";

		}

		httpContext.Response.StatusCode = response.Error;
		await httpContext.Response.WriteAsJsonAsync(response);

	}

}
