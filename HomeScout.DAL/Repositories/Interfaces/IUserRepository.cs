using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PagedList<User>> GetAllPaginatedAsync(UserParameters parameters, ISortHelper<User> sortHelper);
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByUserNameAsync(string userName);
        Task<IEnumerable<User>> GetByRoleAsync(string role);
    }
}
