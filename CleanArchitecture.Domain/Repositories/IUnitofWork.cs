namespace CleanArchitecture.Domain.Repositories
{
    public interface IUnitofWork 
    {
        IStorageRepository StorageRepository { get; }
        IStorageArticleRepository ArticleRepository { get; }
        void Commit();
    }
}
