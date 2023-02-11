namespace FishForoshi.Abstraction.Tools
{
    public interface IPagedList<TData> : IEnumerable<TData>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
    }
}
