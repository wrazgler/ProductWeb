using System;

namespace ProductWeb.Model.Models
{
    public class PageModel
    {
        private const int _pageSize = 10;
        public int PageNumber { get; }
        public int TotalPages { get; }

        public PageModel(int count, int pageNumber)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)_pageSize);
        }

        public bool HasPreviousPage => (PageNumber > 1);

        public bool HasNextPage => (PageNumber < TotalPages);

        public static int GetPageSize()
        {
            return _pageSize;
        }
    }
}
