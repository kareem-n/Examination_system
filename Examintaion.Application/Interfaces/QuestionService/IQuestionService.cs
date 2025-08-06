using Examination.Application.DTOs.Question;
using Examination.Domain.Common;

namespace Examination.Application.Interfaces.QuestionService
{
    public interface IQuestionService
    {
        Task<PageModel<QuestionDto>> GetAllQuestions(GetAllQuestionsParams @params);

        Task<QuestionDto> CreateQuestion(CreateQuestionDto questionDto);


        Task<IEnumerable<QuestionDto>> GetSubjectQuestions(Guid subjectId);

    }
}
