using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Repositories
{
    public class ListingRepository : GenericRepository<Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Listing>> GetAllAsync()
        {
            return await IncludeAllRelations().ToListAsync();
        }

        public override async Task<Listing?> GetByIdAsync(int id)
        {
            return await IncludeAllRelations()
                .FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<IEnumerable<Listing>> GetByTitleAsync(string title)
        {
            return await IncludeAllRelations()
                .Where(l => l.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Listing>> GetByUserIdAsync(string userId)
        {
            return await IncludeAllRelations()
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<PagedList<Listing>> GetAllPaginatedAsync(ListingParameters parameters, ISortHelper<Listing> sortHelper)
        {
            var query = IncludeAllRelations();

            if (!string.IsNullOrWhiteSpace(parameters.Title))
                query = query.Where(l => l.Title.ToLower().Contains(parameters.Title.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.City))
                query = query.Where(l => l.City.ToLower().Contains(parameters.City.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.Address))
                query = query.Where(l => l.Address.ToLower().Contains(parameters.Address.ToLower()));

            if (parameters.Type.HasValue)  
            {
                var typeString = parameters.Type.ToString(); 

                if (!string.IsNullOrWhiteSpace(typeString))  
                    if (Enum.TryParse<ListingType>(typeString, true, out var typeEnum))
                        query = query.Where(l => l.Type == typeEnum);
            }

            if (parameters.MinPrice.HasValue)
                query = query.Where(l => l.Price >= parameters.MinPrice.Value);

            if (parameters.MaxPrice.HasValue)
                query = query.Where(l => l.Price <= parameters.MaxPrice.Value);

            if (parameters.CreatedAfter.HasValue)
                query = query.Where(l => l.CreatedAt >= parameters.CreatedAfter.Value);

            if (parameters.CreatedBefore.HasValue)
                query = query.Where(l => l.CreatedAt <= parameters.CreatedBefore.Value);

            if (!string.IsNullOrWhiteSpace(parameters.UserName))
                query = query.Where(l => l.User.UserName.ToLower().Contains(parameters.UserName.ToLower()));

            query = sortHelper.ApplySort(query, parameters.OrderBy);

            return await PagedList<Listing>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
        }


        private IQueryable<Listing> IncludeAllRelations()
        {
            return dbSet
                .Include(l => l.Photos)
                .Include(l => l.Filters)
                    .ThenInclude(f => f.Filter)
                .Include(l => l.User);
        }
    }
}
