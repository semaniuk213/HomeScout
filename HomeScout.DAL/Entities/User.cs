using Microsoft.AspNetCore.Identity;

namespace HomeScout.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
