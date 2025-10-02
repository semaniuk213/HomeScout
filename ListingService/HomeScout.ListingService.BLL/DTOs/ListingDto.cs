using HomeScout.ListingService.DAL.Entities;

namespace HomeScout.ListingService.BLL.DTOs
{
    public class ListingDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Area { get; set; }
        public ListingType Type { get; set; }
        public string TypeAsString { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public List<PhotoDto> Photos { get; set; } = null!;
        public List<ListingFilterDto> Filters { get; set; } = null!;
    }
}
