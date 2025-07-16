using System.Text.Json.Serialization;

namespace Template.Application.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public string Id { get; set; } = null!;
        public string? AccessToken { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        [JsonIgnore]
        public DateTime RefreshTokenExpires { get; set; }

    }
}
