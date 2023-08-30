using CleanArchitecture.Domain.Entities;
using Dapper;
using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    internal class StorageArticleRepository : BaseRepository
    {
        public StorageArticleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<StorageArticle?> GetById (Guid id, CancellationToken cancellationToken = default)
        {
            var result = await Connection.QueryAsync<StorageArticle>("Select Id, Name, Description FROM [dbo].[StorageArticle] Where Id = @Id",
                                    new { Id = id });
            return result.FirstOrDefault();

        }


    }
}
