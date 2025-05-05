using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class FilterRepository : GenericRepository<Filter>, IFilterRepository
    {
        public FilterRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Filter>> GetFilteredAsync(FilterParameters parameters, ISortHelper<Filter> sortHelper)
        {
            var query = dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Name))
                query = query.Where(f => f.Name.ToLower().Contains(parameters.Name.ToLower()));

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<Filter>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
