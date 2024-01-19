using System.Net;

namespace SmartCache.Domain.Exceptions;
public class CustomException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public CustomException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
