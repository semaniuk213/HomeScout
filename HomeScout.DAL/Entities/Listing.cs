namespace HomeScout.DAL.Entities
{
    public class Listing
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Area { get; set; }
        public ListingType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; } = null!;
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public ICollection<ListingFilter> Filters { get; set; } = new List<ListingFilter>();
    }
}
