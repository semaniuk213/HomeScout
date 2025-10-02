using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.DAL.Repositories.Interfaces
{
    public interface IListingRepository : IGenericRepository<Listing>
    {
        Task<PagedList<Listing>> GetAllPaginatedAsync(ListingParameters parameters, ISortHelper<Listing> sortHelper, CancellationToken cancellationToken = default);
    }
}
