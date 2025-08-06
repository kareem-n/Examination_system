using System.ComponentModel.DataAnnotations.Schema;
using Template.Domain.Common;

namespace Examination.Domain.Models
{
    public class ExamQuestionsAnswer : BaseEntity
    {
        public Guid ExamId { get; set; }

        public Guid QuestionId { get; set; }
        public Guid QuestionAnswerId { get; set; }

        [ForeignKey(nameof(ExamId))]

        public Exam Exam { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        [ForeignKey(nameof(QuestionAnswerId))]
        public QuestionAnswer QuestionAnswer { get; set; }
    }
}
