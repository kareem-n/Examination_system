using Examination.Application.DTOs.Exam;
using Examination.Application.DTOs.ExamDto;
using Examination.Domain.Common;

namespace Examination.Application.Interfaces.ExamService
{
    public interface IExamService
    {
        Task<ExamDto> GenerateStudentExam(Guid subjectId, string userId);

        Task<bool> SubmitExam(Guid examId, ExamAnswers examAnswers);

        Task<PageModel<UserExamDto>> GetUserExams(string userId, UserExamsHistoryParams userExamsHistoryParams);

    }
}
