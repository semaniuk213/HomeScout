using HomeScout.DAL.Data;
using HomeScout.DAL.Repositories.Interfaces;

namespace HomeScout.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private bool disposed = false;
        public IFilterRepository FilterRepository { get; }
        public IListingRepository ListingRepository { get; }
        public IListingFilterRepository ListingFilterRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IOwnerRepository OwnerRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;

            FilterRepository = new FilterRepository(context);
            ListingRepository = new ListingRepository(context);
            ListingFilterRepository = new ListingFilterRepository(context);
            PhotoRepository = new PhotoRepository(context);
            OwnerRepository = new OwnerRepository(context);
        }

        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}