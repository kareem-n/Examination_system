using Examination.Domain.Enums;
using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Models;
using Examination.Infrastructure.Data;
using Examination.Infrastructure.Repostories;
using Microsoft.EntityFrameworkCore;

namespace Examintaion.Infrastructure.Repostories
{
    internal class QuestionsRepo : GenericRepo<Question>, IQuestionRepo
    {

        public QuestionsRepo(AppDbContext context) : base(context)
        {
        }

        // generate a number of random questions based on difficulty and subject percentages  
        public async Task<IEnumerable<Question>> GetRandomQuestionsAsync(
           Guid subjectId,
           int numberOfQuestions,
           short easyPercentage,
           short mediumPercentage,
           short hardPercentage
        )
        {
            var totalPercentage = easyPercentage + mediumPercentage + hardPercentage;
            if (totalPercentage != 100)
            {
                throw new ArgumentException("The sum of percentages must equal 100.");
            }

            var easyCount = (int)Math.Round(numberOfQuestions * (easyPercentage / 100.0));
            var mediumCount = (int)Math.Round(numberOfQuestions * (mediumPercentage / 100.0));
            var hardCount = (int)Math.Round(numberOfQuestions * (hardPercentage / 100.0));


            //_context.Set<Question>().AsNoTracking();

            var easyQuestions = _context.Set<Question>()
                .Include(q => q.Subject)
                .Include(q => q.QuestionAnswers)
                .Where(q => q.SubjectId == subjectId && q.DeletedAt == null && q.DifficultyLevel == DifficultyLevel.Easy)
                .OrderBy(q => Guid.NewGuid())
                .Take(easyCount)
                .ToList()
                ;

            var mediumQuestions = _context.Set<Question>()
                .Include(q => q.Subject)
                .Include(q => q.QuestionAnswers)
                .Where(q => q.SubjectId == subjectId && q.DeletedAt == null && q.DifficultyLevel == DifficultyLevel.Medium)
                .OrderBy(q => Guid.NewGuid())
                .Take(mediumCount)
                .ToList();

            var hardQuestions = _context.Set<Question>()
                .Include(q => q.Subject)
                .Include(q => q.QuestionAnswers)
                .Where(q => q.SubjectId == subjectId && q.DeletedAt == null && q.DifficultyLevel == DifficultyLevel.Hard)
                .OrderBy(q => Guid.NewGuid())
                .Take(hardCount)
                .ToList();

            //return [new Question()];

            return easyQuestions.Concat(mediumQuestions).Concat(hardQuestions);
        }



    }
}
