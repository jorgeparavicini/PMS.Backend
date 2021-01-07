using System.Text.Json.Serialization;

namespace PMS.Backend.Security.Models
{
    public record User
    {
        [JsonPropertyName("username")]
        public string Username { get; init; }
    }
}
