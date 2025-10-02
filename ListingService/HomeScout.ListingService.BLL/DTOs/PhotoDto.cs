namespace HomeScout.ListingService.BLL.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int ListingId { get; set; }
        public string ListingTitle { get; set; } = string.Empty;
    }
}
