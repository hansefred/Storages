using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    internal class StorageArticleRepository : BaseRepository, IStorageArticleRepository
    {
        public StorageArticleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Task<StorageArticle> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
