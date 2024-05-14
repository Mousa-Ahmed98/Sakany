namespace Sakany.Application.DTOS
{
    public class PaginationInfoDTO
    {
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
