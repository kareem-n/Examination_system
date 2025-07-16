using System.ComponentModel.DataAnnotations.Schema;

namespace Examination.Domain.Models
{
    public class RefreshToken
    {
        public string Id { get; set; } = null!;
        public string Token { get; set; } = null!;


        public DateTime Expiration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }


        public bool IsActive => DateTime.UtcNow <= Expiration && RevokedAt == null;


        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; } = null!;
    }
}
