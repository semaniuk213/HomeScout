using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
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