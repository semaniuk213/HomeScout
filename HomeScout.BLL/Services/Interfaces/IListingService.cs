using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
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