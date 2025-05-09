namespace HomeScout.DAL.Entities
{
    public class Listing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public float Area { get; set; }
        public string Type { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<ListingFilter> Filters { get; set; }
    }
}
