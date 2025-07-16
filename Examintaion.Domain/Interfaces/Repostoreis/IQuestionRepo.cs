using Examination.Domain.Models;

namespace Examination.Domain.Interfaces.Repostoreis
{
    public interface IQuestionRepo
    {
        Task<IEnumerable<Question>> GetRandomQuestionsAsync(
          Guid subjectId,
          int numberOfQuestions,
          short easyPercentage,
          short mediumPercentage,
          short hardPercentage
       );
    }
}
