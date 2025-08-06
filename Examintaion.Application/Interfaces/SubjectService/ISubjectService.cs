using Examination.Application.DTOs.Subject;
using Examination.Domain.Common;

namespace Examination.Application.Interfaces.SubjectService
{
    public interface ISubjectService
    {

        /// <summary>
        /// Creates a new subject.
        /// </summary>
        /// <param name="createSubjectDto">The DTO containing the subject details.</param>
        /// <returns>A task that represents the asynchronous operation, containing the created subject DTO.</returns>
        Task<SubjectDto> CreateSubject(CreateSubjectDto createSubjectDto);
        /// <summary>
        /// Retrieves all subjects.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a list of subject DTOs.</returns>
        Task<PageModel<SubjectDto>> GetAllSubjects(GetAllSubjectsParams @params);

        Task<bool> DeleteSubject(Guid subjectId);

        Task<SubjectDto> UpdateSubject(Guid subjectId, UpdateSubjectDto updateSubjectDto);




    }
}
