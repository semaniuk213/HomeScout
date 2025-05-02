namespace HomeScout.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFilterRepository FilterRepository { get; }
        IListingRepository ListingRepository { get; }
        IListingFilterRepository ListingFilterRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IUserRepository UserRepository { get; }
        Task CompleteAsync();
    }
}
