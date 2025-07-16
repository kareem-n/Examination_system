using System.ComponentModel.DataAnnotations;
using Examination.Application.DTOs.QuestionAnswer;
using Examination.Domain.Enums;

namespace Examination.Application.DTOs.Question
{
    public record CreateQuestionDto
    {
        [Required(ErrorMessage = "Question Title is required")]
        [MinLength(10, ErrorMessage = "Question Title minimum length is 10")]
        public string QuestionTitle { get; set; } = null!;

        public DifficultyLevel DifficultyLevel { get; set; } = DifficultyLevel.Medium;

        [Required(ErrorMessage = "Subject Id is required")]
        public Guid SubjectId { get; set; }


        public ICollection<QuestionAnswerDto> Choices { get; set; } = [];


    }
}
