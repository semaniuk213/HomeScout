using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class ListingFilterRepository : GenericRepository<ListingFilter>, IListingFilterRepository
    {
        public ListingFilterRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ListingFilter>> GetByListingIdAsync(int listingId)
        {
            return await dbSet
                .Include(lf => lf.Filter)
                .Include(lf => lf.Listing)
                .Where(lf => lf.ListingId == listingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ListingFilter>> GetByFilterIdAsync(int filterId)
        {
            return await dbSet
                .Include (lf => lf.Filter)
                .Include(lf => lf.Listing)
                .Where(lf => lf.FilterId == filterId)
                .ToListAsync();
        }
    }
}
