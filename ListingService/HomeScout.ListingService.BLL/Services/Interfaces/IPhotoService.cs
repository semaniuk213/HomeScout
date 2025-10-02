using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.BLL.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<PagedList<PhotoDto>> GetAllAsync(PhotoParameters parameters, CancellationToken cancellationToken = default);
        Task<PhotoDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<PhotoDto> CreateAsync(CreatePhotoDto dto, CancellationToken cancellationToken = default);
        Task<PhotoDto> UpdateAsync(int id, UpdatePhotoDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}