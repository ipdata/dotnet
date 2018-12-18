namespace IpData.Models
{
    public sealed class ApiError
    {
        public ApiError()
        {
        }

        public ApiError(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
