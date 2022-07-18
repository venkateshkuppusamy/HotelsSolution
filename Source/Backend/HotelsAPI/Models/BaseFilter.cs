namespace HotelsAPI.Models
{
    public class BaseFilter
    {
        public string? SortField { get; set; }
        public bool IsSortDesc { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
