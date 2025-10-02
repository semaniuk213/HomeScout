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

        // Explicit loading
        public override async Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var photo = await dbSet.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (photo != null)
            {
                await context.Entry(photo)
                    .Reference(p => p.Listing)
                    .LoadAsync(cancellationToken);
            }

            return photo;
        }

        public override async Task<IEnumerable<Photo>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var photos = await dbSet.ToListAsync(cancellationToken);

            foreach (var photo in photos)
            {
                await context.Entry(photo)
                    .Reference(p => p.Listing)
                    .LoadAsync(cancellationToken);
            }

            return photos;
        }

        // Eager Loading
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