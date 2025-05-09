namespace HomeScout.DAL.Parameters
{
    public class PhotoParameters : QueryStringParameters
    {
        public int? ListingId { get; set; }
        public string? Url { get; set; }
    }
}
