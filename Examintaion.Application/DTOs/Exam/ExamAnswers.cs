using System.ComponentModel.DataAnnotations;

namespace Examination.Application.DTOs.Exam
{
    public class ExamAnswers
    {
        [Required]
        public ICollection<ExamQuestionAnswer> ExamQuestionsAnswers { get; set; } = [];

    }
}
