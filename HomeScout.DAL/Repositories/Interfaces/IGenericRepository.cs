using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task<PagedList<T>> FindAsync(QueryStringParameters parameters, ISortHelper<T> sortHelper);
        void Remove(T entity);
    }
}
