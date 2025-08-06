using Examination.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examintaion.Infrastructure.Configurations
{
    internal class ExamQuestionsAnswersConfiguration : IEntityTypeConfiguration<ExamQuestionsAnswer>
    {
        public void Configure(EntityTypeBuilder<ExamQuestionsAnswer> builder)
        {
            builder.HasKey(e => new { e.ExamId, e.QuestionId, e.QuestionAnswerId });

            builder
                .HasOne(e => e.Exam)
                .WithMany(e => e.ExamQuestionsAnswers)
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(e => e.Question)
                .WithMany(q => q.ExamQuestionsAnswers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(e => e.QuestionAnswer)
                .WithMany(q => q.ExamQuestionsAnswers)
                .HasForeignKey(e => e.QuestionAnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ExamQuestionsAnswers");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("getdate()");

        }
    }
}
