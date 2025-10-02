using HomeScout.ListingService.DAL.Data;
using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;
using HomeScout.ListingService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.ListingService.DAL.Repositories
{
    public class ListingFilterRepository : GenericRepository<ListingFilter>, IListingFilterRepository
    {
        public ListingFilterRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<ListingFilter>> GetAllPaginatedAsync(
            ListingFilterParameters parameters,
            ISortHelper<ListingFilter> sortHelper,
            CancellationToken cancellationToken = default)
        {
            var query = dbSet
                .Include(lf => lf.Filter)
                .Include(lf => lf.Listing)
                .AsQueryable();

            if (parameters.ListingId.HasValue)
                query = query.Where(lf => lf.ListingId == parameters.ListingId.Value);

            if (parameters.FilterId.HasValue)
                query = query.Where(lf => lf.FilterId == parameters.FilterId.Value);

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<ListingFilter>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize, cancellationToken);
        }
    }
}