using System.Linq.Expressions;
using Examination.Domain.Common;

namespace Template.Domain.Interfaces.Repostoreis
{
    public interface ISpecification<T> where T : class
    {
        List<Expression<Func<T, bool>>>? Criteria { get; protected set; }

        List<Expression<Func<T, object>>>? Includes { get; protected set; }

        List<SortOption<T>>? SortOptions { get; protected set; }

        Expression<Func<T, object>>? Projection { get; protected set; }

        int? PageNumber { get; protected set; }

        int? PageSize { get; protected set; }

        public bool IsPaged { get; set; }

    }
}
