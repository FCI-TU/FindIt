using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FindIt.Application.ErrorHandling
{
	public enum StatusCode
	{
		Continue = 100,
		SwitchingProtocols = 101,
		Processing = 102,
		EarlyHints = 103,
		Ok = 200,
		Created = 201,
		Accepted = 202,
		NonAuthoritativeInformation = 203,
		NoContent = 204,
		ResetContent = 205,
		PartialContent = 206,
		MultiStatus = 207,
		AlreadyReported = 208,
		ImUsed = 226,
		MultipleChoices = 300,
		MovedPermanently = 301,
		Found = 302,
		SeeOther = 303,
		NotModified = 304,
		UseProxy = 305,
		TemporaryRedirect = 307,
		PermanentRedirect = 308,
		BadRequest = 400,
		Unauthorized = 401,
		PaymentRequired = 402,
		Forbidden = 403,
		NotFound = 404,
		MethodNotAllowed = 405,
		NotAcceptable = 406,
		ProxyAuthenticationRequired = 407,
		RequestTimeout = 408,
		Conflict = 409,
		Gone = 410,
		LengthRequired = 411,
		PreconditionFailed = 412,
		PayloadTooLarge = 413,
		UriTooLong = 414,
		UnsupportedMediaType = 415,
		RequestedRangeNotSatisfiable = 416,
		ExpectationFailed = 417,
		AmATeapot = 418,
		InsufficientSpaceOnResource = 419,
		MethodFailure = 420,
		DestinationLocked = 421,
		UnprocessableEntity = 422,
		Locked = 423,
		FailedDependency = 424,
		TooEarly = 425,
		UpgradeRequired = 426,
		PreconditionRequired = 428,
		TooManyRequests = 429,
		RequestHeaderFieldsTooLarge = 431,
		UnavailableForLegalReasons = 451,
		InternalServerError = 500,
		NotImplemented = 501,
		BadGateway = 502,
		ServiceUnavailable = 503,
		GatewayTimeout = 504,
		HttpVersionNotSupported = 505,
		VariantAlsoNegotiates = 506,
		InsufficientStorage = 507,
		LoopDetected = 508,
		BandwidthLimitExceeded = 509,
		NotExtended = 510,
		NetworkAuthenticationRequired = 511,
	}
	public class Status
	{
        public static Status None { get; } = new(200, "The request was successful.");

        private static readonly Dictionary<int, string> HttpStatusCodeMessage = new Dictionary<int, string> {
            { 100 , "Continue: The server has received the request headers and the client should proceed to send the request body."},
               { 101 , "Switching Protocols: The requester has asked the server to switch protocols, and the server has agreed to do so."},
               { 102 , "Processing: The server has received and is processing the request, but no response is available yet."},
               { 103 , "Early Hints: The server is sending some response headers before the final response is ready."},
               { 200 , "OK: The request was successful."},
               { 201 , "Created: The request has succeeded, and a new resource has been created as a result."},
               { 202 , "Accepted: The request has been accepted for processing, but the processing has not been completed."},
               { 203 , "Non-Authoritative Information: The request was successful but the returned metadata may be from a different source."},
               { 204 , "No Content: The request was successful, but there is no content to send in the response."},
               { 205 , "Reset Content: The request was successful, and the client should reset the view that sent the request."},
               { 206 , "Partial Content: The server is delivering only part of the resource due to a range header sent by the client."},
               { 207 , "Multi-Status: Provides status for multiple independent operations."},
               { 208 , "Already Reported: The members of a DAV binding have already been enumerated in a previous reply."},
               { 226 , "IM Used: The server has fulfilled the request using a different instance-manipulation operation."},
               { 300 , "Multiple Choices: There are multiple options for the resource that the client may follow."},
               { 301 , "Moved Permanently: The resource has been moved to a different URL permanently."},
               { 302 , "Found: The resource resides temporarily at a different URL."},
               { 303 , "See Other: The server is redirecting the client to a different resource."},
               { 304 , "Not Modified: The resource has not been modified since the last request."},
               { 305 , "Use Proxy: The requested resource is available only through a proxy."},
               { 307 , "Temporary Redirect: The resource resides temporarily at a different URL."},
               { 308 , "Permanent Redirect: The resource has been permanently moved to a new URL."},
               { 400 , "Bad Request: The server cannot process the request due to a client error (e.g., malformed request syntax)."},
               { 401 , "Unauthorized: The request requires user authentication."},
               { 402 , "Payment Required: Reserved for future use."},
               { 403 , "Forbidden: The server understood the request but refuses to authorize it."},
               { 404 , "Not Found: The server can't find the requested resource."},
               { 405 , "Method Not Allowed: The request method is not supported for the requested resource."},
               { 406 , "Not Acceptable: The requested resource is only capable of generating unacceptable content."},
               { 407 , "Proxy Authentication Required: The client must first authenticate with the proxy."},
               { 408 , "Request Timeout: The server timed out waiting for the request."},
               { 409 , "Conflict: The request could not be completed due to a conflict with the current state of the resource."},
               { 410 , "Gone: The requested resource is no longer available and will not be available again."},
               { 411 , "Length Required: The server requires a Content-Length header."},
               { 412 , "Precondition Failed: One or more conditions given in the request header fields were not met."},
               { 413 , "Payload Too Large: The request is larger than the server is willing or able to process."},
               { 414 , "URI Too Long: The URI provided was too long for the server to process."},
               { 415 , "Unsupported Media Type: The media format of the requested data is not supported by the server."},
               { 416 , "Range Not Satisfiable: The range specified in the request cannot be fulfilled."},
               { 417 , "Expectation Failed: The expectation given in the request could not be met by the server."},
               { 418 , "I'm a teapot: This code was defined in 1998 as an April Fools' joke. It is not expected to be implemented by actual HTTP servers."},
               { 419 , "Insufficient Space On Resource: The server is unable to store the representation needed to complete the request."},
               { 420 , "Method Failure: The server met a failure while handling the request."},
               { 421 , "Destination Locked: The destination resource is locked."},
               { 422 , "Unprocessable Entity: The server understands the content type of the request entity, but the syntax is incorrect."},
               { 423 , "Locked: The resource that is being accessed is locked."},
               { 424 , "Failed Dependency: The request failed due to failure of a previous request."},
               { 425 , "Too Early: The server is unwilling to process a request that might be replayed."},
               { 426 , "Upgrade Required: The client should switch to a different protocol."},
               { 428 , "Precondition Required: The origin server requires the request to be conditional."},
               { 429 , "Too Many Requests: The user has sent too many requests in a given amount of time."},
               { 431 , "Request Header Fields Too Large: The server is unwilling to process the request because its header fields are too large."},
               { 451 , "Unavailable For Legal Reasons: The server is denying access to the resource as a consequence of a legal demand."},
               { 500 , "Internal Server Error: The server encountered an unexpected condition that prevented it from fulfilling the request."},
               { 501 , "Not Implemented: The server does not support the functionality required to fulfill the request."},
               { 502 , "Bad Gateway: The server, while acting as a gateway or proxy, received an invalid response from the upstream server."},
               { 503 , "Service Unavailable: The server is not ready to handle the request."},
               { 504 , "Gateway Timeout: The server, while acting as a gateway or proxy, did not get a response in time from the upstream server."},
               { 505 , "HTTP Version Not Supported: The HTTP version used in the request is not supported by the server."},
               { 506 , "Variant Also Negotiates: The server has an internal configuration error."},
               { 507 , "Insufficient Storage: The server is unable to store the representation needed to complete the request."},
               { 508 , "Loop Detected: The server detected an infinite loop while processing the request."},
               { 509 , "Bandwidth Limit Exceeded: The server has exceeded the bandwidth specified by the server administrator."},
               { 510 , "Not Extended: Further extensions to the request are required for the server to fulfill it."},
               { 511 , "Network Authentication Required: The client needs to authenticate to gain network access."},
        };
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public Status(int statusCode, string? message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetHttpMessage(statusCode);
		}
		public Status(StatusCode statusCode, string? message = null)
		{
			StatusCode = ((int)statusCode);
			Message = message ?? GetHttpMessage(((int)statusCode));
		}
		public bool IsSuccess()
		{
			return StatusCode is >= 200 and < 300;
		}
		public bool IsError()
		{
			return !IsSuccess();
		}

		public static string GetHttpMessage(int statusCode)
		{
			return HttpStatusCodeMessage.GetValueOrDefault(statusCode) ?? "Invalid HTTP status code!";

		}
	}
}
