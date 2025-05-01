namespace HomeScout.DAL.Entities
{
    public class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public ICollection<ListingFilter> Listings { get; set; }
    }
}
