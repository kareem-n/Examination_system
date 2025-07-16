using Examination.Application.DTOs.ExamDto;

namespace Examination.Application.Interfaces.ExamService
{
    public interface IExamService
    {
        Task<ExamDto> GenerateStudentExam(Guid subjectId, string userId);

    }
}
