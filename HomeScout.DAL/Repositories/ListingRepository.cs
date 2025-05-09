using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class ListingRepository : GenericRepository<Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Listing>> GetAllAsync()
        {
            return await IncludeAllRelations().ToListAsync();
        }

        public override async Task<Listing?> GetByIdAsync(int id)
        {
            return await IncludeAllRelations()
                .FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<IEnumerable<Listing>> GetByTitleAsync(string title)
        {
            return await IncludeAllRelations()
                .Where(l => l.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Listing>> GetByCityAsync(string city)
        {
            return await IncludeAllRelations()
                .Where(l => l.City.ToLower() == city.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Listing>> GetByUserIdAsync(string userId)
        {
            return await IncludeAllRelations()
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Listing>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await IncludeAllRelations()
                .Where(l => l.Price >= minPrice && l.Price <= maxPrice)
                .ToListAsync();
        }

        private IQueryable<Listing> IncludeAllRelations()
        {
            return dbSet
                .Include(l => l.Photos)
                .Include(l => l.Filters)
                    .ThenInclude(f => f.Filter)
                .Include(l => l.User);
        }
    }
}
