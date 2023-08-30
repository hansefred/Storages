using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories
{
    public interface IStorageArticleRepository : IRepository<StorageArticle>
    {
        public Task<StorageArticle> GetById(Guid id);
    }
}
