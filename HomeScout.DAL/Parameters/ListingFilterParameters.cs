namespace HomeScout.DAL.Parameters
{
    public class ListingFilterParameters : QueryStringParameters
    {
        public int? ListingId { get; set; }
        public int? FilterId { get; set; }
    }
}
