using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoDto>> GetAllAsync();
        Task<IEnumerable<PhotoDto>> GetByListingIdAsync(int listingId);
        Task<PagedList<PhotoDto>> GetAllPaginatedAsync(PhotoParameters parameters);
        Task<PhotoDto> GetByIdAsync(int id);
        Task<PhotoDto> CreateAsync(CreatePhotoDto dto);
        Task<PhotoDto> UpdateAsync(int id, UpdatePhotoDto dto);
        Task DeleteAsync(int id);
    }
}
