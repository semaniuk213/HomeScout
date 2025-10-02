namespace HomeScout.ListingService.BLL.DTOs
{
    public class CreatePhotoDto
    {
        public string Url { get; set; } = string.Empty;
        public int ListingId { get; set; }
    }
}
