using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IListingFilterRepository : IGenericRepository<ListingFilter>
    {
        Task<PagedList<ListingFilter>> GetAllPaginatedAsync(ListingFilterParameters parameters, ISortHelper<ListingFilter> sortHelper, CancellationToken cancellationToken = default);
    }
}