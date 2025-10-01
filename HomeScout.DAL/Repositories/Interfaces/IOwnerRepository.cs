using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<PagedList<Owner>> GetAllPaginatedAsync(OwnerParameters parameters, ISortHelper<Owner> sortHelper, CancellationToken cancellationToken = default);
    }
}
