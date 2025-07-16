using Examination.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examintaion.Infrastructure.Configurations
{
    internal class SubjectConfigurationExamConfig : IEntityTypeConfiguration<SubjectExamConfiguration>
    {
        public void Configure(EntityTypeBuilder<SubjectExamConfiguration> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.NumberOsQuestions)
                .IsRequired()
                .HasColumnType("smallint");

            builder.Property(p => p.Easy)
                .IsRequired()
                .HasColumnType("smallint");

            builder.Property(p => p.Miduiem)
                .IsRequired()
                .HasColumnType("smallint");

            builder.Property(p => p.Hard)
                .IsRequired()
                .HasColumnType("smallint");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Subject)
                .WithOne(p => p.SubjectConfiguration)
                .HasForeignKey<SubjectExamConfiguration>(p => p.SubjectId);




        }
    }
}
