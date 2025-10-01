using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
{
    public interface IListingFilterService
    {
        Task<PagedList<ListingFilterDto>> GetAllAsync(ListingFilterParameters parameters, CancellationToken cancellationToken = default);
        Task<ListingFilterDto> CreateAsync(CreateListingFilterDto dto, CancellationToken cancellationToken = default);
        Task<ListingFilterDto> UpdateAsync(int id, UpdateListingFilterDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}