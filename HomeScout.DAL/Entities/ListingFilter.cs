namespace HomeScout.DAL.Entities
{
    public class ListingFilter
    {
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public int FilterId { get; set; }
        public Filter Filter { get; set; }
    }
}
