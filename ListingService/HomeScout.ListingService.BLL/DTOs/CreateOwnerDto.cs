namespace HomeScout.ListingService.BLL.DTOs
{
    public class CreateOwnerDto
    {
        public string Name { get; set; } = string.Empty;       
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
