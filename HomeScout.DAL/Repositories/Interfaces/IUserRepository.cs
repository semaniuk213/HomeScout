using HomeScout.DAL.Entities;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetByFullNameAsync(string fullName);
        Task<IEnumerable<User>> GetByRoleAsync(string role);
    }
}
