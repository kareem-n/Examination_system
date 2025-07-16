using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examination.Infrastructure.Configurations.Auth
{
    public class RoleSeeding : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            ICollection<IdentityRole> roles =
            [
                new IdentityRole("admin")
                {
                    Id = "6102b6f4-4691-4483-92f4-cd35f07a6daa",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "3960dfdd-144f-492e-a6a5-a9358d41e33c"
                } ,
                new IdentityRole("student")
                {
                    Id = "d0ce1f56-6f05-464f-9dfb-fa705437eb20",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = "05d7e578-1bde-4db4-ad53-2cb691c3405a"
                }
            ];

            builder.HasData(roles);
        }
    }
}
