using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetByFullNameAsync(string username)
        {
            return await dbSet
                .Where(u => u.UserName.ToLower().Contains(username.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        {
            return await dbSet
                .Where(u => u.Role.ToLower() == role.ToLower())
                .ToListAsync();
        }
    }
}
