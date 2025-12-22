using System.Security.Cryptography;
using System.Text.Json;

namespace Olympus.Core.Web;

public static class EntityTag {

	/// <summary>
	/// Normalizes an ETag value by removing surrounding double quotes.
	/// </summary>
	/// <remarks>
	/// Returns an empty string if the input is null or empty.
	/// </remarks>
	public static string From(string? value) {

		if (string.IsNullOrEmpty(value)) return string.Empty;

		return value.Trim('"');

	}

	/// <summary>
	/// Generates a normalized ETag value from a Guid.
	/// </summary>
	/// <remarks>
	/// Uses the invariant "N" format and upper case.
	/// Returns an empty string if the input is null.
	/// </remarks>
	public static string From(Guid? value) {

		if (value is null) return string.Empty;

		return From(value?.ToString("N").ToUpper());

	}

	/// <summary>
	/// Generates a normalized ETag value from an object.
	/// </summary>
	/// <remarks>
	/// Serializes the object to JSON and computes a SHA256 hash.
	/// Returns the hash as an upper-case hexadecimal string.
	/// </remarks>
	public static string From<T>(T data) {

		var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(data);

		var hashBytes = SHA256.HashData(jsonBytes);

		return $"{Convert.ToHexString(hashBytes)}";

	}

	/// <summary>
	/// Evaluates the HTTP If-Match precondition.
	/// </summary>
	/// <remarks>
	/// Answers: "Is the operation allowed to proceed?"
	/// True means the ETag matches (or wildcard '*').
	/// False means a conflict should occur.
	/// </remarks>
	public static bool IfMatch(string? etag, string? value) {

		if (string.IsNullOrEmpty(etag)) return false;

		if (From(etag) == "*" || string.IsNullOrEmpty(value)) return true;

		return From(etag) == From(value);

	}

	/// <summary>
	/// Evaluates the HTTP If-Match precondition using a Guid-based ETag.
	/// </summary>
	/// <remarks>
	/// Answers: "Is the operation allowed to proceed?"
	/// True means the ETag matches (or wildcard '*').
	/// False means a conflict should occur.
	/// </remarks>
	public static bool IfMatch(string? etag, Guid? value) {

		if (string.IsNullOrEmpty(etag)) return false;

		if (From(etag) == "*" || value is null) return true;

		return From(etag) == From(value);

	}

	/// <summary>
	/// Evaluates the HTTP If-None-Match condition for cache validation.
	/// </summary>
	/// <remarks>
	/// Answers: "Was the resource NOT modified?"
	/// True means the resource was not modified (should return 304 Not Modified).
	/// False means the resource was modified (should return 200 OK).
	/// </remarks>
	public static bool IfNoneMatch(string? etag, string? value) {

		if (string.IsNullOrEmpty(etag) || string.IsNullOrEmpty(value)) return false;

		if (From(etag) == "*") return false;

		return From(etag) == From(value);

	}

	/// <summary>
	/// Evaluates the HTTP If-None-Match condition for cache validation using a Guid-based ETag.
	/// </summary>
	/// <remarks>
	/// Answers: "Was the resource NOT modified?"
	/// True means the resource was not modified (should return 304 Not Modified).
	/// False means the resource was modified (should return 200 OK).
	/// </remarks>
	public static bool IfNoneMatch(string? etag, Guid? value) {

		if (string.IsNullOrEmpty(etag) || value is null) return false;

		if (From(etag) == "*") return false;

		return From(etag) == From(value);

	}

}
