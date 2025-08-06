using System.ComponentModel.DataAnnotations.Schema;
using Template.Domain.Common;

namespace Examination.Domain.Models
{
    public class QuestionAnswer : BaseEntity
    {
        public string AnswerText { get; set; } = null!;

        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; } = null!;

        public List<ExamQuestionsAnswer> ExamQuestionsAnswers { get; set; }

    }
}
