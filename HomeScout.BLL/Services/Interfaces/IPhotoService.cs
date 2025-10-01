using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
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