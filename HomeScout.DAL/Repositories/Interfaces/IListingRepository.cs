using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IListingRepository : IGenericRepository<Listing>
    {
        Task<PagedList<Listing>> GetAllPaginatedAsync(ListingParameters parameters, ISortHelper<Listing> sortHelper, CancellationToken cancellationToken = default);
    }
}
