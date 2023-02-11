namespace FishForoshi.Implementation.Tools
{
    using FishForoshi.Abstraction.Tools;
    using System.Collections.Generic;
    using System.Linq;

    public class PagedList<TData> : List<TData>, IPagedList<TData>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PagedList(
            IEnumerable<TData> source,
            int pageNumber,
            int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = source.Count();
            TotalPages = TotalItems / PageSize;
            if (TotalItems % PageSize > 0) TotalPages++;

            AddRange(source.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }
    }
}
