using System.ComponentModel.DataAnnotations;

namespace Examination.Application.DTOs.QuestionAnswer
{
    public class QuestionAnswerDto
    {
        [Required(ErrorMessage = "Answer Text is required")]
        public string AnswerTxt { get; set; } = null!;

        [Required]
        public bool IsCorrect { get; set; }

    }
}
