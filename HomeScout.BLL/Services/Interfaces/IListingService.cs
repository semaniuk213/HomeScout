using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
{
    public interface IListingService
    {
        Task<IEnumerable<ListingDto>> GetAllAsync();
        Task<PagedList<ListingDto>> GetAllPaginatedAsync(ListingParameters parameters);
        Task<ListingDto> GetByIdAsync(int id);
        Task<IEnumerable<ListingDto>> GetByTitleAsync(string title);
        Task<IEnumerable<ListingDto>> GetByUserIdAsync(string userId);
        Task<ListingDto> CreateAsync(CreateListingDto dto);
        Task<ListingDto> UpdateAsync(int id, UpdateListingDto dto);
        Task DeleteAsync(int id);
    }
}
