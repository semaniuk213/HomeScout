using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.DAL.Repositories.Interfaces
{
    public interface IListingFilterRepository : IGenericRepository<ListingFilter>
    {
        Task<PagedList<ListingFilter>> GetAllPaginatedAsync(ListingFilterParameters parameters, ISortHelper<ListingFilter> sortHelper, CancellationToken cancellationToken = default);
    }
}