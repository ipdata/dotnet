using System.Text.Json.Serialization;

namespace IPData.Models
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

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
