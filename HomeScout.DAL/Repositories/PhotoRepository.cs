using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Photo?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(p => p.Listing)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await dbSet 
                .Include(p => p.Listing)
                .ToListAsync();
        }

        public async Task<IEnumerable<Photo>> GetByListingIdAsync(int listingId)
        {
            return await dbSet
                .Include(p => p.Listing)
                .Where(p => p.ListingId == listingId)
                .ToListAsync();
        }

        public async Task<PagedList<Photo>> GetAllPaginatedAsync(PhotoParameters parameters, ISortHelper<Photo> sortHelper)
        {
            var query = dbSet
                .Include(p => p.Listing)
                .AsQueryable();

            if (parameters.ListingId.HasValue)
                query = query.Where(p => p.ListingId == parameters.ListingId.Value);

            if (!string.IsNullOrWhiteSpace(parameters.Url))
                query = query.Where(p => p.Url.ToLower().Contains(parameters.Url.ToLower()));

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<Photo>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
        }

    }
}
