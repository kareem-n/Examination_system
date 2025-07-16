using Examination.Application.DTOs.QuestionAnswer;

namespace Examination.Application.DTOs.Exam
{
    public class ExamQuestionDto
    {

        public Guid Id { get; set; }
        public string QuestionTxt { get; set; } = null!;
        public List<QuestionOptionDto> Options { get; set; } = [];

    }
}
