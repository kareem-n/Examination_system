using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 chars.")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Confirm Password does not match")]
        [Column("Cofirm Password")]
        public string CPassword { get; set; } = string.Empty;



    }
}
