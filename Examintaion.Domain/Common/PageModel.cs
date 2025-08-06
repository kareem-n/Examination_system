namespace Examination.Domain.Common
{
    public class PageModel<T>
    {

        public int Count { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);

        public bool hasNext => currentPage < TotalPages;

        public bool hasPrevious => currentPage > 1;

        public int currentPage { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<T> Items { get; set; } = null!;


        public PageModel() { }
        public PageModel(IEnumerable<T> items, int count, int currentPage, int pageSize)
        {
            Items = items;
            Count = count;
            this.currentPage = currentPage;
            PageSize = pageSize;
        }


    }
}
