using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IFilterRepository : IGenericRepository<Filter>
    {
        Task<PagedList<Filter>> GetFilteredAsync(FilterParameters parameters, ISortHelper<Filter> sortHelper);
    }
}
