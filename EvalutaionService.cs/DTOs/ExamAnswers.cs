using System.ComponentModel.DataAnnotations;

namespace EvalutaionService.cs.DTOs
{
    public class ExamAnswers
    {
        [Required]
        public ICollection<ExamQuestionAnswer> ExamQuestionsAnswers { get; set; } = [];

    }
}
