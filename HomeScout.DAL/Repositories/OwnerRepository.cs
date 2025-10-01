using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;

namespace HomeScout.DAL.Repositories
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<Owner>> GetAllPaginatedAsync(
            OwnerParameters parameters,
            ISortHelper<Owner> sortHelper,
            CancellationToken cancellationToken = default)
        {
            var query = dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Name))
                query = query.Where(o => o.Name.ToLower().Contains(parameters.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.Email))
                query = query.Where(o => o.Email!.ToLower().Contains(parameters.Email.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.Phone))
                query = query.Where(o => o.Phone!.ToLower().Contains(parameters.Phone.ToLower()));

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<Owner>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize, cancellationToken);
        }
    }
}