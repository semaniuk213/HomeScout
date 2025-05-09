using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IListingRepository : IGenericRepository<Listing>
    {
        Task<IEnumerable<Listing>> GetByTitleAsync(string title);
        Task<IEnumerable<Listing>> GetByUserIdAsync(string userId);
        Task<PagedList<Listing>> GetAllPaginatedAsync(ListingParameters parameters, ISortHelper<Listing> sortHelper);
    }
}
