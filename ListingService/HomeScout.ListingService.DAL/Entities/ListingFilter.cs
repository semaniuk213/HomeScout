namespace HomeScout.ListingService.DAL.Entities
{
    public class ListingFilter
    {
        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;
        public int FilterId { get; set; }
        public Filter Filter { get; set; } = null!;
    }
}
