using System.Linq.Expressions;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Infrastructure.Specifications
{
    public abstract class Spicification<T> : ISpecification<T> where T : class
    {

        public List<Expression<Func<T, bool>>>? Criteria { get; set; } = [];
        public List<Expression<Func<T, object>>>? Includes { get; set; }
        public uint? Take { get; set; }
        public uint? Skip { get; set; }
        public Expression<Func<T, object>>? Projection { get; set; }
        public void AddIncludes(List<Expression<Func<T, object>>> inludes)
        {
            Includes = inludes;
        }

        public void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria!.Add(criteria);
        }

        public void AddPagging(uint take, uint skip)
        {
            Take = take;
            Skip = skip;
        }


        public void AddProjection(Expression<Func<T, object>> projection)
        {
            Projection = projection;
        }


    }
}
