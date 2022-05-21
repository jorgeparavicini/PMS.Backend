using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMS.Backend.Security.Models.Requests
{
    public record LoginRequest
    {
        [JsonPropertyName("username")]
        [Required]
        public string Username { get; init; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; init; }

        [JsonPropertyName("rememberMe")]
        [Required]
        public bool RememberMe { get; init; } = false;
    }
}
