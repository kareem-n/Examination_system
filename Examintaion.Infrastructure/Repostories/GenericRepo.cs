using System.Linq.Expressions;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Common;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Infrastructure.Repostories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {

        protected readonly AppDbContext _context;
        public GenericRepo(AppDbContext context)
        {
            _context = context;
        }


        public async Task<T?> AddAsync(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            entity.DeletedAt = DateTime.UtcNow.ToLocalTime();

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<T>? specification = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            // Filter out deleted entities
            query = query.Where(t => t.DeletedAt == null);

            if (specification != null)
            {
                // Apply criteria filters
                if (specification.Criteria != null && specification.Criteria.Any())
                {
                    foreach (var criteria in specification.Criteria)
                    {
                        query = query.Where(criteria);
                    }
                }

                // Apply includes for navigation properties
                if (specification.Includes != null && specification.Includes.Any())
                {
                    foreach (var include in specification.Includes)
                    {
                        query = query.Include(include);
                    }
                }

                // Apply pagination
                if (specification.Skip.HasValue)
                {
                    query = query.Skip((int)specification.Skip.Value);
                }

                if (specification.Take.HasValue)
                {
                    query = query.Take((int)specification.Take.Value);
                }

                // Apply projection if specified
                if (specification.Projection != null)
                {
                    return await query.Select(specification.Projection).Cast<TResult>().ToListAsync();
                }
            }

            // Default projection to TResult if no specification provided
            return await query.Cast<TResult>().ToListAsync();
        }


        public async Task<T?> GetByIdAsync(Guid id, List<Expression<Func<T, object>>> inlcude = null!)
        {
            if (id == Guid.Empty)
            {
                return null!;
            }

            IQueryable<T> query = _context.Set<T>();
            query = query.Where(t => t.DeletedAt == null);
            if (inlcude != null)
            {
                foreach (var inc in inlcude)
                {
                    query = query.Include(inc);
                }
            }


            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }


        public async Task<T?> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                return null!;
            }
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity!;
        }
    }
}
