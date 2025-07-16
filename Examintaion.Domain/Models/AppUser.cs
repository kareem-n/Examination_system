using Microsoft.AspNetCore.Identity;

namespace Examination.Domain.Models
{
    public class AppUser : IdentityUser<string>
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

        public ICollection<Exam> Exams { get; set; }
    }
}
