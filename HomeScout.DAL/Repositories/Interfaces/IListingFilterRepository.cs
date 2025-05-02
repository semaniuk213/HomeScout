using HomeScout.DAL.Entities;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IListingFilterRepository : IGenericRepository<ListingFilter>
    {
        Task<IEnumerable<ListingFilter>> GetByListingIdAsync(int listingId);
        Task<IEnumerable<ListingFilter>> GetByFilterIdAsync(int filterId);
    }
}
