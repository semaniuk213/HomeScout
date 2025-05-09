using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
{
    public interface IListingFilterService
    {
        Task<IEnumerable<ListingFilterDto>> GetByListingIdAsync(int listingId);
        Task<IEnumerable<ListingFilterDto>> GetByFilterIdAsync(int filterId);
        Task<PagedList<ListingFilterDto>> GetAllPaginatedAsync(ListingFilterParameters parameters);
        Task<ListingFilterDto> CreateAsync(CreateListingFilterDto dto);
        Task<ListingFilterDto> UpdateAsync(int id, UpdateListingFilterDto dto);
        Task DeleteAsync(int id);
    }
}
