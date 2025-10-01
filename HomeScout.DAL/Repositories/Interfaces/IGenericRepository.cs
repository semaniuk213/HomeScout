﻿using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        Task<PagedList<T>> FindAsync(QueryStringParameters parameters, ISortHelper<T> sortHelper, CancellationToken cancellationToken = default);
        void Remove(T entity);
    }
}