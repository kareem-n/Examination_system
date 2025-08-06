//using System.Data.Entity;
using Examination.Domain.Models;
using Examination.Infrastructure.Data;
using Examination.Infrastructure.Repostories;
using Microsoft.EntityFrameworkCore;

namespace Examintaion.Infrastructure.Repostories
{
    public class ExamRepo : GenericRepo<Exam>
    {
        public ExamRepo(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Exam>> GetExamsAsync(Guid subjectId, string studenId)
        {
            return await _context.Set<Exam>()
                .Include(e => e.Questions)
                .ThenInclude(q => q.QuestionAnswers)
                .Where(e => e.SubjectId == subjectId && studenId == e.StudentId)
                .ToListAsync();
        }

        public async Task<int> GetUserExamsCount(string userId)
        {
            return await _context.Set<Exam>()
                .Where(e => e.StudentId == userId && e.DeletedAt == null)
                .CountAsync();
        }

        public async Task<object> GetTopSubjectsExam()
        {

            return await _context.Set<Exam>()
                .GroupBy(g => g.SubjectId)
                .Select(g => new
                {
                    ExamCount = g.Count(),
                    SubjectName = g.First().Subject.Title
                })
                .ToListAsync();

        }


        public async Task<IEnumerable<Exam>> GetExamsRate()
        {
            return await _context.Set<Exam>()
                .Where(s => s.Score > 0)
                .ToListAsync();

        }


        public async Task<IEnumerable<string>> GetExamCorrectAnswersIDs(string examId)
        {
            return await _context.Set<Exam>()
                .Where(e => e.Id.ToString() == examId)
                .SelectMany(e => e.Questions.SelectMany(q => q.QuestionAnswers.Where(a => a.IsCorrect).Select(a => a.Id.ToString())))
                .ToListAsync();

        }


    }
}
