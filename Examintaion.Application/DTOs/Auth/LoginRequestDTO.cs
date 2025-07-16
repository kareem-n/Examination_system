using System.ComponentModel.DataAnnotations;

namespace Template.Application.DTOs.Auth
{
    public record LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;
    }


}
