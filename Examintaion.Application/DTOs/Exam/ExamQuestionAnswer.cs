using System.ComponentModel.DataAnnotations;

namespace Examination.Application.DTOs.Exam
{
    public class ExamQuestionAnswer
    {
        [Required]
        public string QuestionId { get; set; }
        public string? AnswerId { get; set; }
    }
}
