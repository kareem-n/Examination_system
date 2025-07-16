namespace Template.API.Response
{
    public class ApiResponse<T> where T : class
    {
        public int StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public T Data { get; set; } = null!;

        public IList<dynamic> Errors { get; set; } = [];

        public ApiResponse(int statusCode, string message, T data, bool success)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            IsSuccess = success;
        }

        public static ApiResponse<T> Success(int statusCode, string message, T data)
        {
            return new ApiResponse<T>(statusCode, message, data, true);
        }

        public static ApiResponse<T> Success(int statusCode, string message)
        {
            return new ApiResponse<T>(statusCode, message, null!, true);
        }

        public static ApiResponse<T> Error(int statusCode, string message, IList<object> errors)
        {
            return new ApiResponse<T>(statusCode, message, null!, false)
            {
                Errors = errors
            };
        }

        public static ApiResponse<T> Error(int statusCode, string message)
        {
            return new ApiResponse<T>(statusCode, message, null!, false)
            {
                Errors = []
            };
        }


    }
}
