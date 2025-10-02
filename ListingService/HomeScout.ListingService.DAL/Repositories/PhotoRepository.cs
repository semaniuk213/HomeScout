using HomeScout.ListingService.DAL.Data;
using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;
using HomeScout.ListingService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.ListingService.DAL.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await dbSet
                .Include(p => p.Listing)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public override async Task<IEnumerable<Photo>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet
                .Include(p => p.Listing)
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedList<Photo>> GetAllPaginatedAsync(
            PhotoParameters parameters,
            ISortHelper<Photo> sortHelper,
            CancellationToken cancellationToken = default)
        {
            var query = dbSet
                .Include(p => p.Listing)
                .AsQueryable();

            if (parameters.ListingId.HasValue)
                query = query.Where(p => p.ListingId == parameters.ListingId.Value);

            if (!string.IsNullOrWhiteSpace(parameters.Url))
                query = query.Where(p => p.Url.ToLower().Contains(parameters.Url.ToLower()));

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<Photo>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize, cancellationToken);
        }
    }
}