using Examination.Application.DTOs.Exam;
using Examination.Domain.Models;
using Examination.Infrastructure.Specifications;

namespace Examination.Application.Specifications
{
    public class UserExamsSpecification : Spicification<Exam>
    {
        public UserExamsSpecification(string id, UserExamsHistoryParams userExamsHistoryParams)
        {

            AddCriteria(x => x.StudentId == id);

            AddIncludes([e => e.Subject]);

            AddPagging(userExamsHistoryParams.PageSize, userExamsHistoryParams.PageNumber);

        }
    }
}
