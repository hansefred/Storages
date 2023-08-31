using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories
{
    public interface IStorageArticleRepository
    {
        Task<StorageArticle?> GetById(Guid id, CancellationToken cancellationToken);
        Task Update(StorageArticle storageArticle, CancellationToken cancellationToken = default);
    }
}