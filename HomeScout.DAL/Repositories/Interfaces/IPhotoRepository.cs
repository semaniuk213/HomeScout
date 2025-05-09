using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IPhotoRepository : IGenericRepository<Photo>
    {
        Task<IEnumerable<Photo>> GetByListingIdAsync(int listingId);
        Task<PagedList<Photo>> GetAllPaginatedAsync(PhotoParameters parameters, ISortHelper<Photo> sortHelper);
    }
}
