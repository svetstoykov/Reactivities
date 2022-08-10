using API.Common.Models;

namespace API.Common.Middleware.ErrorHandling
{
    public class ExceptionResponseModel : BaseApiModel
    {
        private ExceptionResponseModel(int statusCode, string message, string details = null)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Details = details;
        }

        public int StatusCode { get; }

        public string Message { get; }

        public string Details { get; }

        public static ExceptionResponseModel New(int statusCode, string message, string details = null) =>
            new(statusCode, message, details);
    }
}
