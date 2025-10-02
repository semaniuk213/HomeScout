namespace HomeScout.ListingService.BLL.DTOs
{
    public class ListingFilterDto
    {
        public int ListingId { get; set; }
        public string ListingTitle { get; set; } = string.Empty;
        public int FilterId { get; set; }
        public string FilterName { get; set; } = string.Empty;
    }
}
