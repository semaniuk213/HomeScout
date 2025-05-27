using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PagedList<User>> GetAllPaginatedAsync(UserParameters parameters, ISortHelper<User> sortHelper)
        {
            var query = dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.UserName))
                query = query.Where(u => u.UserName.ToLower().Contains(parameters.UserName.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.Email))
                query = query.Where(u => u.Email.ToLower().Contains(parameters.Email.ToLower()));

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<User>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
