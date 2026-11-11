namespace beeproperties.admin.webapi.models.DTO.Helper
{
    public class SuccessResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public int StatusCode { get; set; } = 200;

        public SuccessResponse() { }

        public SuccessResponse(T data, string message = "Success", int statusCode = 200)
        {
            Success = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }
    }

    public class SuccessResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; } = 200;

        public SuccessResponse() { }

        public SuccessResponse(string message = "Success", int statusCode = 200)
        {
            Success = true;
            Message = message;
            StatusCode = statusCode;
        }
    }
}

