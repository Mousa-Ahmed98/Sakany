namespace Sakany.Application.Helper
{
    public class PaginatedList
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;


        public PaginatedList() { }

        public PaginatedList(int totalitems, int page, int pagesize = 6)
        {

            TotalItems = totalitems;
            PageSize = pagesize;

            TotalPages = (int)Math.Ceiling((decimal)totalitems / pagesize);
            CurrentPage = Math.Min(Math.Max(page, 1), TotalPages);

            int maxPagesToShow = 10;
            int halfMaxPagesToShow = maxPagesToShow / 2;

            StartPage = Math.Max(CurrentPage - halfMaxPagesToShow, 1);
            EndPage = Math.Min(StartPage + maxPagesToShow - 1, TotalPages);
            if (EndPage == TotalPages)
            {
                StartPage = Math.Max(TotalPages - maxPagesToShow + 1, 1);
            }




        }

    }

}
