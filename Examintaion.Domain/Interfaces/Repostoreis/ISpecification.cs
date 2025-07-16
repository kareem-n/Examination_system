using System.Linq.Expressions;

namespace Template.Domain.Interfaces.Repostoreis
{
    public interface ISpecification<T> where T : class
    {
        List<Expression<Func<T, bool>>>? Criteria { get; protected set; }

        List<Expression<Func<T, object>>>? Includes { get; protected set; }

        Expression<Func<T, object>>? Projection { get; protected set; }

        uint? Take { get; protected set; }

        uint? Skip { get; protected set; }

    }
}
