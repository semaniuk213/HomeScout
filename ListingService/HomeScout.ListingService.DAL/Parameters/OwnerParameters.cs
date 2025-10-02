namespace HomeScout.ListingService.DAL.Parameters
{
    public class OwnerParameters : QueryStringParameters
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
