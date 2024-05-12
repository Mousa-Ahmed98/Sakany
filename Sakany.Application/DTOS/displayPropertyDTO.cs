namespace Sakany.Application.DTOS
{
    public class displayPropertyDTO
    {
        public int id { get; set; }
        public float price { get; set; }
        public int numOfRooms { get; set; }
        public int numOfBathrooms { get; set; }
        public float area { get; set; }
        public int govID { get; set; }
        public int cityId { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string status { get; set; }
        public string Address { get; set; }
        //Owner info
        public string? ownerImageUrl { get; set; } = "/images/ProfileImage/Avatar.svg";
        public string? ownerName { get; set; }
        public string? postAddedTime { get; set; } = "Just Now";
    }
}
