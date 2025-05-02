using HomeScout.DAL.Entities;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IFilterRepository : IGenericRepository<Filter>
    {
        Task<Filter?> GetByNameAsync(string name);
    }
}
