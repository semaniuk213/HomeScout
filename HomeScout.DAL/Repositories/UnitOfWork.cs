using HomeScout.DAL.Data;
using HomeScout.DAL.Repositories.Interfaces;

namespace HomeScout.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IFilterRepository FilterRepository { get; }
        public IListingRepository ListingRepository { get; }
        public IListingFilterRepository ListingFilterRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;

            FilterRepository = new FilterRepository(context);
            ListingRepository = new ListingRepository(context);
            ListingFilterRepository = new ListingFilterRepository(context);
            PhotoRepository = new PhotoRepository(context);
            UserRepository = new UserRepository(context);
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
