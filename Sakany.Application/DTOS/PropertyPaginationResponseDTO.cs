namespace Sakany.Application.DTOS
{
    public class PropertyPaginationResponseDTO
    {
        public List<displayPropertyDTO> Properties { get; set; }
        public PaginationInfoDTO PaginationInfo { get; set; }
    }
}
