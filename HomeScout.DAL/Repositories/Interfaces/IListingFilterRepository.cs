using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IListingFilterRepository : IGenericRepository<ListingFilter>
    {
        Task<IEnumerable<ListingFilter>> GetByListingIdAsync(int listingId);
        Task<IEnumerable<ListingFilter>> GetByFilterIdAsync(int filterId);
        Task<PagedList<ListingFilter>> GetAllPaginatedAsync(ListingFilterParameters parameters, ISortHelper<ListingFilter> sortHelper);
    }
}
