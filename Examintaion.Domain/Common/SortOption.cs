using System.Linq.Expressions;

namespace Examination.Domain.Common
{
    public class SortOption<T>
    {

        public Expression<Func<T, object>> SortKey { get; set; }

        public bool IsDescending { get; set; }

        public SortOption(Expression<Func<T, object>> sortKey, bool isDescending = false)
        {
            SortKey = sortKey;
            IsDescending = isDescending;
        }

    }
}
