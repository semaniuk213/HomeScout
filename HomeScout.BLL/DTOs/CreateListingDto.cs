using HomeScout.DAL.Entities;

namespace HomeScout.BLL.DTOs
{
    public class CreateListingDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public float Area { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
    }
}
