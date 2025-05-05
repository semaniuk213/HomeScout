using HomeScout.DAL.Data;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;
        protected readonly DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public async Task<PagedList<T>> FindAsync(QueryStringParameters parameters, ISortHelper<T> sortHelper)
        {
            IQueryable<T> query = dbSet;

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<T>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
