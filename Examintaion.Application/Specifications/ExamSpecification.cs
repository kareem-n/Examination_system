using System.Linq.Expressions;
using Examination.Domain.Models;
using Examination.Infrastructure.Specifications;

namespace Examination.Application.Specifications
{
    public class ExamSpecification : Spicification<Exam>
    {
        public ExamSpecification(Expression<Func<Exam, bool>> expression)
        {
            AddIncludes([e => e.Questions, e => e.Questions.Select(q => q.QuestionAnswers)]);
            AddCriteria(expression);

        }




    }
}
