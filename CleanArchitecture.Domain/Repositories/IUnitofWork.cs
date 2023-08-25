namespace CleanArchitecture.Domain.Repositories
{
    public interface IUnitofWork 
    {
        IStorageRepository StorageRepository { get; }
        void Commit();
    }
}
