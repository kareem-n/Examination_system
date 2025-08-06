using Examination.Application.DTOs.Question;
using Examination.Application.DTOs.QuestionAnswer;
using Examination.Domain.Models;
using Examination.Infrastructure.Specifications;

namespace Examination.Application.Specifications
{
    public class QuestionSpecification : Spicification<Question>
    {

        public QuestionSpecification(GetAllQuestionsParams @params)
        {


            if (@params.SubjectId != null && @params.SubjectId != Guid.Empty)
            {
                AddCriteria(q => q.SubjectId == @params.SubjectId);
            }


            if (@params.PageNumber > 0 && @params.PageSize > 0)
            {
                AddPagging(@params.PageSize, @params.PageNumber);
            }


            AddProjection(q => new QuestionDto
            {
                Id = q.Id.ToString(),
                QuestionTitle = q.QuestionText,
                SubjectName = q.Subject.Title,
                QuestionAnswerDtos = q.QuestionAnswers.Select(a => new QuestionAnswerDto
                {
                    AnswerTxt = a.AnswerText,
                    IsCorrect = a.IsCorrect
                }).ToList()
            });



        }

    }
}
