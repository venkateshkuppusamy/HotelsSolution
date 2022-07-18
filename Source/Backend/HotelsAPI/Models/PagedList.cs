namespace HotelsAPI.Models
{
    public class PagedList<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int Count { get; set; }

        public List<T> Items { get; set; }

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            Count = items.Count;
            Items = items;

        }
    }
}
