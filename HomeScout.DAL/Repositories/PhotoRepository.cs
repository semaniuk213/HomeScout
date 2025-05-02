using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Photo>> GetByListingIdAsync(int listingId)
        {
            return await dbSet
                .Where(p => p.ListingId == listingId)
                .ToListAsync();
        }
    }
}
