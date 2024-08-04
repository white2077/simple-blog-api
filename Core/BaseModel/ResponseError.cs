namespace AspNetCoreRestfulApi.Core.BaseModel
{
    public class ResponseError
    {

        public object? StatusCode { get; set; }
        public object? Message { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        public ResponseError(object? statusCode, object? message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
