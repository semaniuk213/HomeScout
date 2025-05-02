using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class FilterRepository : GenericRepository<Filter>, IFilterRepository
    {
        public FilterRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Filter?> GetByNameAsync(string name)
        {
            return await dbSet
                .FirstOrDefaultAsync(f => f.Name == name);
        }
    }
}
