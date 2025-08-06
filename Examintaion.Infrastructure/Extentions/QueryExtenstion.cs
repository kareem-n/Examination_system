using System.Data.Entity;
using Examination.Domain.Common;

namespace Examintaion.Infrastructure.Extentions
{
    public static class QueryExtenstion
    {

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, SortOption<T> sortOption)
        {

            if (sortOption.IsDescending)
            {
                return query.OrderByDescending(sortOption.SortKey);
            }
            else
            {
                return query.OrderBy(sortOption.SortKey);
            }

        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int pageSize, int pagenumber)
        {
            if (pageSize <= 0 || pageSize <= 0)
            {
                return query;
            }
            return query.Skip((pagenumber - 1) * pageSize)
                        .Take(pageSize);
        }







    }
}
