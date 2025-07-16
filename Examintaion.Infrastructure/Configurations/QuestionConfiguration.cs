using Examination.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examintaion.Infrastructure.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(q => q.DifficultyLevel)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(q => q.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.HasOne(q => q.Subject)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // exam relation  
            builder.HasMany(q => q.Exams)
                .WithMany(e => e.Questions)
                .UsingEntity(j => j.ToTable("ExamQuestion"));
        }
    }
}
