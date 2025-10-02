using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.BLL.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<PagedList<OwnerDto>> GetAllAsync(OwnerParameters parameters, CancellationToken cancellationToken = default);
        Task<OwnerDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<OwnerDto> CreateAsync(CreateOwnerDto dto, CancellationToken cancellationToken = default);
        Task<OwnerDto> UpdateAsync(int id, UpdateOwnerDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}