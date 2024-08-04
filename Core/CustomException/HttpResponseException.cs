namespace AspNetCoreRestfulApi.Core.CustomException
{
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; }

        public object? Value { get; }

        public HttpResponseException(int statusCode, object? value)
        {
            StatusCode = statusCode;
            Value = value;
        }

    }
}
