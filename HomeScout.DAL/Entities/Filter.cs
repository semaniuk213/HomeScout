namespace HomeScout.DAL.Entities
{
    public class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ListingFilter>? Listings { get; set; } 
    }
}
