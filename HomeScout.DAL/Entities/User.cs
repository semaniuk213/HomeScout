using Microsoft.AspNetCore.Identity;

namespace HomeScout.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public List<Listing> Listings { get; set; } = new ();
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
