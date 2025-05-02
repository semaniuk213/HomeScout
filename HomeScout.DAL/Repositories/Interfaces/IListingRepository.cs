using HomeScout.DAL.Entities;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IListingRepository : IGenericRepository<Listing>
    {
        Task<IEnumerable<Listing>> GetByTitleAsync(string title);
        Task<IEnumerable<Listing>> GetByCityAsync(string city);
        Task<IEnumerable<Listing>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Listing>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
