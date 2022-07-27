using Models.Enumerations;

namespace Models.Common
{
    public class Result<T>
    {
        private Result(T data, ResultType type, bool isSuccessful = false, string message = null)
        {
            Data = data;
            ResultType = type;
            Message = message ?? string.Empty;
            IsSuccessful = isSuccessful;
        }

        public bool IsSuccessful { get; private set; }

        public T Data { get; private set; }

        public string Message { get; private set; }

        public ResultType ResultType { get; private set; }

        public static Result<T> Success(T data, string message = null) => new(data, ResultType.Success, true, message);

        public static Result<T> Failure(string message = null) => new(default, ResultType.Failure, false, message);

        public static Result<T> NotFound(string message = null) => new(default, ResultType.NotFound, false, message);
     
        public static Result<T> Unauthorized(string message = null) => new(default, ResultType.Unauthorized, false, message);

        public static Result<T> New(T data, ResultType type, bool isSuccessful, string message) => new(data, type, isSuccessful, message);
    }
}
