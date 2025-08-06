using Examination.Application.DTOs.Dashboard;
using Examination.Application.Interfaces;
using Examination.Domain.Models;
using Examintaion.Infrastructure.Repostories;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Application.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ExamRepo examRepo;
        private readonly IGenericRepo<Subject> subjectsRepo;

        public DashboardService(
            UserManager<AppUser> userManager,
            ExamRepo examRepo,
            IGenericRepo<Subject> SubjectsRepo
            )
        {
            this.userManager = userManager;
            this.examRepo = examRepo;
            subjectsRepo = SubjectsRepo;
        }

        public async Task<object> GetExamsRate()
        {
            var result = await examRepo.GetExamsRate();
            var success = result.Where(e => e.Score > e.Questions.Count / 2).Count();
            var fail = result.Where(e => e.Score < e.Questions.Count / 2).Count();

            return new { success, fail };
        }

        public async Task<object> GetTopSubjectsExams()
        {


            var topSubjectsExams = await examRepo.GetTopSubjectsExam();

            if (topSubjectsExams == null)
            {
                return null!;
            }

            return topSubjectsExams!;

        }

        public async Task<TotalsDto> GetTotalStatus()
        {

            var scores = await examRepo.GetExamsRate();





            return new TotalsDto
            {
                TotalSubjects = await subjectsRepo.GetCountAsync(),
                TotalExams = await examRepo.GetCountAsync(),
                TotalUsers = userManager.Users.Count(),
                AVGScore = scores.Average(s => (double)s.Score),
                MaxScore = scores.Max(s => (double)s.Score),
                MinScore = scores.Min(s => (double)s.Score),
            };
        }

        public async Task<UsersNumbersDto> GetUserNumbers()
        {
            var totalUsers = userManager.Users.Count();
            var usersPerMonth = userManager.Users
                .GroupBy(u => new { u.CreatedAt.Year, u.CreatedAt.Month })
                .Select(g => new UserPerMonth
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                    UserCount = g.Count()
                })
                //.OrderBy(m => DateTime.ParseExact(m.Month, "MMMM", CultureInfo.InvariantCulture).Month)
                .ToList();

            return new UsersNumbersDto
            {
                TotalUsers = totalUsers,
                Users = usersPerMonth.ToList()
            };




        }
    }
}
