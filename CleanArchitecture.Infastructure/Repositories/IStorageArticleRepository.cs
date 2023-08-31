using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infastructure.Repositories
{
    internal interface IStorageArticleRepository
    {
        Task<StorageArticle?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task Update(StorageArticle storageArticle, CancellationToken cancellationToken = default);

        Task Add(StorageArticle storageArticle, CancellationToken cancellationToken = default);
    }
}