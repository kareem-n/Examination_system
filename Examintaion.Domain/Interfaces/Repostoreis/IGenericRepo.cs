using System.Linq.Expressions;
using Template.Domain.Common;

namespace Template.Domain.Interfaces.Repostoreis
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<T>? specification = null);
        Task<T?> GetByIdAsync(Guid id, List<Expression<Func<T, object>>> expression = null!);
        Task<T?> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);

        Task<int> GetCountAsync(ISpecification<T> specification = null!);
    }
}
