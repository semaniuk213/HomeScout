using HomeScout.BLL.DTOs;

namespace HomeScout.BLL.Services.Interfaces
{
    public interface IFilterService
    {
        Task<IEnumerable<FilterDto>> GetAllAsync();
        Task<FilterDto?> GetByIdAsync(int id);
        Task<FilterDto> CreateAsync(CreateFilterDto dto);
        Task<FilterDto?> UpdateAsync(UpdateFilterDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
