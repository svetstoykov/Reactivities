namespace Models.Common
{
    public class Result<T>
    {

        private Result(T data, bool isSuccessful = false, string message = null)
        {
            Data = data;
            Message = message ?? string.Empty;
            IsSuccessful = isSuccessful;
        }


        public bool IsSuccessful { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public static Result<T> Success(T value, string message = null) => new(value, true, message);

        public static Result<T> Failure(string message) => new(default, false, message);
    }
}
