using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infastructure.Exeptions;
using Dapper;
using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    internal class StorageArticleRepository : BaseRepository, IStorageArticleRepository
    {
        public StorageArticleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        internal async Task Add(StorageArticle storageArticle, CancellationToken cancellationToken = default)
        {
            await Connection.ExecuteAsync("INSERT INTO [dbo].[StorageArticle] (Id,Name, Description)VALUES (NEWID(), @Name, @Description)",
                             new { Name = storageArticle.ArticleName, Description = storageArticle.Description }, transaction: Transaction);

        }

        public async Task Update(StorageArticle storageArticle, CancellationToken cancellationToken = default)
        {
            var result = await Connection.ExecuteAsync("Update [dbo].[StorageArticle] Set Name = @Name, Description = @Description Where Id = @Id",
                                    new { Name = storageArticle.ArticleName, Description = storageArticle.Description, Id = storageArticle.Id });

            if (result < 1)
            {
                throw new StorageArticleUpdateFailedInfastructureException($"StorageArticle with ID: {storageArticle.Id} not found");
            }
        }

        public async Task<StorageArticle?> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Connection.QueryAsync<StorageArticle>("Select Id, Name, Description FROM [dbo].[StorageArticle] Where Id = @Id",
                        new { Id = id });
            return result.FirstOrDefault();
        }


    }
}
