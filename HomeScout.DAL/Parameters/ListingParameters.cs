using HomeScout.DAL.Entities;

namespace HomeScout.DAL.Parameters
{
    public class ListingParameters : QueryStringParameters
    {
        public string? Title { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public ListingType? Type { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
    }
}
