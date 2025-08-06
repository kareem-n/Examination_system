using System.Linq.Expressions;
using Examination.Domain.Common;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Infrastructure.Specifications
{
    public abstract class Spicification<T> : ISpecification<T> where T : class
    {

        public List<Expression<Func<T, bool>>>? Criteria { get; set; } = [];
        public List<Expression<Func<T, object>>>? Includes { get; set; }
        public List<SortOption<T>>? SortOptions { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool IsPaged { get; set; }
        public Expression<Func<T, object>>? Projection { get; set; }
        protected void AddIncludes(List<Expression<Func<T, object>>> inludes)
        {
            Includes = inludes;
        }

        protected void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria!.Add(criteria);
        }

        protected void AddPagging(int pageSize, int pageNumber)
        {
            IsPaged = true;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }


        protected void AddProjection(Expression<Func<T, object>> projection)
        {
            Projection = projection;
        }
        // add sort
        protected void AddSort(SortOption<T> sort)
        {
            SortOptions ??= [];
            SortOptions.Add(sort);
        }




    }
}
