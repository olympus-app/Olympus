using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Olympus.Core.Backend.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ProducesResponseAttribute : ProducesResponseTypeAttribute {

	public ProducesResponseAttribute(HttpStatusCode statusCode) : base(statusCode.Value) { }

	public ProducesResponseAttribute(HttpStatusCode statusCode, Type type) : base(type, statusCode.Value) { }

	public ProducesResponseAttribute(HttpStatusCode statusCode, Type type, string contentType = ContentTypes.Json) : base(type, statusCode.Value, contentType) { }

}
