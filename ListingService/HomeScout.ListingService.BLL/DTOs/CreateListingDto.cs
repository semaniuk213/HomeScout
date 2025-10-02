namespace HomeScout.ListingService.BLL.DTOs
{
    public class CreateListingDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Area { get; set; }
        public string Type { get; set; } = string.Empty;
        public int OwnerId { get; set; }
    }
}
