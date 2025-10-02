using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.DAL.Repositories.Interfaces
{
    public interface IPhotoRepository : IGenericRepository<Photo>
    {
        Task<PagedList<Photo>> GetAllPaginatedAsync(PhotoParameters parameters, ISortHelper<Photo> sortHelper, CancellationToken cancellationToken = default);
    }
}
