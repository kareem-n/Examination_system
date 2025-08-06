using Examination.Application.DTOs.QuestionAnswer;

namespace Examination.Application.DTOs.Question
{
    public class QuestionDto
    {
        public string Id { get; set; } = null!;

        public string QuestionTitle { get; set; } = null!;

        public string SubjectName { get; set; } = null!;

        public List<QuestionAnswerDto> QuestionAnswerDtos { get; set; } = [];
    }
}
