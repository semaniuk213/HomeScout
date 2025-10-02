using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.DAL.Repositories.Interfaces
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<PagedList<Owner>> GetAllPaginatedAsync(OwnerParameters parameters, ISortHelper<Owner> sortHelper, CancellationToken cancellationToken = default);
    }
}
