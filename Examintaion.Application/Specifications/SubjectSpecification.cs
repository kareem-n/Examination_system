using Examination.Application.DTOs.Subject;
using Examination.Domain.Models;
using Examination.Infrastructure.Specifications;

namespace Examination.Application.Specifications
{
    public class SubjectSpecification : Spicification<Subject>
    {

        public SubjectSpecification(GetAllSubjectsParams @params)
        {

            if (@params.PageIndex > 0 && @params.PageSize > 0)
                AddPagging((uint)@params.PageSize, (uint)((@params.PageIndex - 1) * @params.PageSize));


            AddProjection(sub => new SubjectDto
            {
                Id = sub.Id.ToString(),
                Description = sub.Description,
                Title = sub.Title,
            });

        }

    }
}
