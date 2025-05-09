using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
{
    public interface IFilterService
    {
        Task<IEnumerable<FilterDto>> GetAllAsync();
        Task<FilterDto> GetByIdAsync(int id);
        Task<FilterDto> CreateAsync(CreateFilterDto dto);
        Task<FilterDto> UpdateAsync(int id, UpdateFilterDto dto);
        Task<PagedList<FilterDto>> GetFilteredAsync(FilterParameters parameters);
        Task DeleteAsync(int id);
    }
}
