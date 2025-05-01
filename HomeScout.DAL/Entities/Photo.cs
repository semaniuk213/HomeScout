namespace HomeScout.DAL.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
    }
}
