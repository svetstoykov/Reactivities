using System.Net;

namespace Models.Common
{
    public class ExceptionResponse
    {
        private ExceptionResponse(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; }

        public string Message { get; }

        public string Details { get; }

        public static ExceptionResponse New(int statusCode, string message, string details = null) =>
            new(statusCode, message, details);
    }
}
