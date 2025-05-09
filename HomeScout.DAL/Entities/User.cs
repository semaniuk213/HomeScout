namespace HomeScout.DAL.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        public string Role { get; set; } = "User";
        public ICollection<Listing> Listings { get; set; }
    }
}
