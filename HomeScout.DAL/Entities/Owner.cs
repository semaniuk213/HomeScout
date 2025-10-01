namespace HomeScout.DAL.Entities
{
    public class Owner
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<Listing> Listings { get; set; } = new();
    }
}
