using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;

namespace HomeScout.ListingService.DAL.Repositories.Interfaces
{
    public interface IFilterRepository : IGenericRepository<Filter>
    {
        Task<PagedList<Filter>> GetFilteredAsync(FilterParameters parameters, ISortHelper<Filter> sortHelper, CancellationToken cancellationToken = default);
    }
}