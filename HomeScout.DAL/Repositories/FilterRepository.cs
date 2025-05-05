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
    }
}
