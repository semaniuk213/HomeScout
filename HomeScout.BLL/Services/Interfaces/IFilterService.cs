using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
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