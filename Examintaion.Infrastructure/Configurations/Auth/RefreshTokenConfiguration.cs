using Examination.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examination.Infrastructure.Configurations.Auth
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {

            builder.HasKey(k => new { k.Id, k.UserId });

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(t => t.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(t => t.UserId)
                .HasConstraintName("FK_RefreshTokens_Users_UserId");

        }
    }
}
