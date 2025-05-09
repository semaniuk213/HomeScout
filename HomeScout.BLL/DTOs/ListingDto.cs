using HomeScout.DAL.Entities;

namespace HomeScout.BLL.DTOs
{
    public class ListingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public float Area { get; set; }
        public ListingType Type { get; set; }
        public string TypeAsString { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; } 
        public List<PhotoDto> Photos { get; set; }
        public List<ListingFilterDto> Filters { get; set; }
    }
}
