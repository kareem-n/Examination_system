using Examination.Application.DTOs.Dashboard;

namespace Examination.Application.Interfaces
{
    public interface IDashboardService
    {

        Task<UsersNumbersDto> GetUserNumbers();

        Task<object> GetTopSubjectsExams();
        Task<object> GetExamsRate();
        Task<TotalsDto> GetTotalStatus();


    }
}
