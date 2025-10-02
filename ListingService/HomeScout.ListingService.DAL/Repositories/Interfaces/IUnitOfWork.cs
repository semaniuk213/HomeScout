namespace HomeScout.ListingService.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFilterRepository FilterRepository { get; }
        IListingRepository ListingRepository { get; }
        IListingFilterRepository ListingFilterRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IOwnerRepository OwnerRepository { get; }
        Task CompleteAsync(CancellationToken cancellationToken = default);
    }
}