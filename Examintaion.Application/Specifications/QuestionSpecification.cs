using Examination.Application.DTOs.Question;
using Examination.Domain.Models;
using Examination.Infrastructure.Specifications;

namespace Examination.Application.Specifications
{
    public class QuestionSpecification : Spicification<Question>
    {

        public QuestionSpecification(GetAllQuestionsParams @params)
        {


            if (@params.SubjectId != Guid.Empty)
            {
                AddCriteria(q => q.SubjectId == @params.SubjectId);
            }


            if (@params.PageIndex > 0 && @params.PageSize > 0)
            {
                AddPagging((uint)@params.PageSize, (uint)((@params.PageIndex - 1) * @params.PageSize));
            }


            AddProjection(q => new QuestionDto
            {
                Id = q.Id.ToString(),
                QuestionTitle = q.QuestionText,
            });



        }

    }
}
