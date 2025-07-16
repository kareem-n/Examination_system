namespace Examination.Application.DTOs.QuestionAnswer
{
    public class QuestionOptionDto
    {

        public Guid Id { get; set; }

        public string AnswerTxt { get; set; } = null!;

        public bool IsCorrect { get; set; }

    }
}
