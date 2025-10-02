using HomeScout.ListingService.DAL.Data;
using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;
using HomeScout.ListingService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.ListingService.DAL.Repositories
{
    public class ListingRepository : GenericRepository<Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Listing>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await IncludeAllRelations().ToListAsync(cancellationToken);
        }

        public override async Task<Listing?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await IncludeAllRelations()
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public async Task<PagedList<Listing>> GetAllPaginatedAsync(
            ListingParameters parameters,
            ISortHelper<Listing> sortHelper,
            CancellationToken cancellationToken = default)
        {
            var query = IncludeAllRelations();

            if (!string.IsNullOrWhiteSpace(parameters.Title))
                query = query.Where(l => l.Title.ToLower().Contains(parameters.Title.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.City))
                query = query.Where(l => l.City.ToLower().Contains(parameters.City.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.Address))
                query = query.Where(l => l.Address.ToLower().Contains(parameters.Address.ToLower()));

            if (parameters.Type.HasValue)
                query = query.Where(l => l.Type == parameters.Type.Value);

            if (parameters.MinPrice.HasValue)
                query = query.Where(l => l.Price >= parameters.MinPrice.Value);

            if (parameters.MaxPrice.HasValue)
                query = query.Where(l => l.Price <= parameters.MaxPrice.Value);

            if (parameters.CreatedAfter.HasValue)
                query = query.Where(l => l.CreatedAt >= parameters.CreatedAfter.Value);

            if (parameters.CreatedBefore.HasValue)
                query = query.Where(l => l.CreatedAt <= parameters.CreatedBefore.Value);

            if (parameters.OwnerId.HasValue)
                query = query.Where(l => l.OwnerId == parameters.OwnerId.Value);

            if (!string.IsNullOrWhiteSpace(parameters.OwnerName))
                query = query.Where(l => l.Owner.Name.ToLower().Contains(parameters.OwnerName.ToLower()));

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<Listing>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize, cancellationToken);
        }

        private IQueryable<Listing> IncludeAllRelations()
        {
            return dbSet
                .Include(l => l.Photos)
                .Include(l => l.Filters)
                    .ThenInclude(f => f.Filter)
                .Include(l => l.Owner);
        }
    }
}