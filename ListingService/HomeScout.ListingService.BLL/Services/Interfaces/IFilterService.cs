using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.BLL.Services.Interfaces
{
    public interface IFilterService
    {
        Task<PagedList<FilterDto>> GetAllAsync(FilterParameters parameters, CancellationToken cancellationToken = default);
        Task<FilterDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<FilterDto> CreateAsync(CreateFilterDto dto, CancellationToken cancellationToken = default);
        Task<FilterDto> UpdateAsync(int id, UpdateFilterDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}