using Microsoft.AspNetCore.Identity;

namespace Examination.Domain.Models
{
    public class AppUser : IdentityUser<string>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool isActive { get; set; } = true;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

        public ICollection<Exam> Exams { get; set; }
    }
}
