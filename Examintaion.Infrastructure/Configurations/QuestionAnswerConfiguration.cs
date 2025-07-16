using Examination.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examintaion.Infrastructure.Configurations
{
    internal class QuestionAnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {

            builder.ToTable("QuestionAnswers");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.AnswerText)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(q => q.IsCorrect)
                .IsRequired();

            builder.Property(q => q.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.HasOne(q => q.Question)
                .WithMany(q => q.QuestionAnswers)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
