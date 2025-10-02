using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.BLL.Services.Interfaces
{
    public interface IListingService
    {
        Task<PagedList<ListingDto>> GetAllAsync(ListingParameters parameters, CancellationToken cancellationToken = default);
        Task<ListingDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ListingDto> CreateAsync(CreateListingDto dto, CancellationToken cancellationToken = default);
        Task<ListingDto> UpdateAsync(int id, UpdateListingDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}