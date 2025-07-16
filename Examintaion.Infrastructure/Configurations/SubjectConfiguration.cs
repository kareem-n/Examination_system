using Examination.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examintaion.Infrastructure.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {

            builder.HasKey(p => p.Id);
            //
            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();
            //
            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("varchar(100)");
            //
            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");
            //
            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            //


        }
    }
}
