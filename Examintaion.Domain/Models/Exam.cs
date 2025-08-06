using System.ComponentModel.DataAnnotations.Schema;
using Examination.Domain.Enums;
using Template.Domain.Common;

namespace Examination.Domain.Models
{
    public class Exam : BaseEntity
    {
        [ForeignKey(nameof(StudentId))]
        public string StudentId { get; set; }
        public AppUser Student { get; set; }
        //

        [ForeignKey(nameof(SubjectId))]
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ExamStudentState Status { get; set; } = ExamStudentState.Pending;

        public DateTime StartedAt { get; set; }
        public DateTime? SubmitedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public decimal Score { get; set; }

        public List<Question> Questions { get; set; } = [];
        public List<ExamQuestionsAnswer> ExamQuestionsAnswers { get; set; } = [];

    }
}
