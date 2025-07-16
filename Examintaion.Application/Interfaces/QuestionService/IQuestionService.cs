using Examination.Application.DTOs.Question;

namespace Examination.Application.Interfaces.QuestionService
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestions(GetAllQuestionsParams @params);

        Task<QuestionDto> CreateQuestion(CreateQuestionDto questionDto);


        Task<IEnumerable<QuestionDto>> GetSubjectQuestions(Guid subjectId);

    }
}
