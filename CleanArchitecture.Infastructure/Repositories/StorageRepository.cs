using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infastructure.Exeptions;
using Dapper;
using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    internal class StorageRepository : BaseRepository, IStorageRepository
    {
        private readonly StorageArticleRepository _storageArticleRepository;

        public StorageRepository(IDbTransaction dbTransaction, StorageArticleRepository storageArticleRepository) : base(dbTransaction)
        {
            _storageArticleRepository = storageArticleRepository;
        }

        public async  Task Add(Storage entity, CancellationToken cancellationToken = default)
        {
            await Connection.ExecuteAsync("INSERT INTO [dbo].[Storage] (Id,Name, Description)VALUES (NEWID(), @Name, @Description)",
                                    new { Name = entity.Name, Description = entity.Description }, transaction: Transaction);
        }

        public async Task Delete(Storage entity, CancellationToken cancellationToken = default)
        {
            await Connection.ExecuteAsync("DELETE [dbo].[Storage] Where Id = @Id", new {Id = entity.Id},
                                            transaction: Transaction);
        }

        public async Task<List<Storage>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await Connection.QueryAsync<Storage>("Select Id, Name, Description FROM [dbo].[Storage]");
            if (result is null) 
            {
                return new();
            }

            return result.ToList();
        }

        public async Task<Storage?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await Connection.QueryAsync<Storage>("Select Id, Name, Description FROM [dbo].[Storage] Where Id = @Id",
                                                                new {Id = id});

            return result.FirstOrDefault();
        }

        public async Task Update(Storage storage, CancellationToken cancellationToken = default)
        {
            if (storage.StorageArticles.Any()) 
            {
                foreach (var article in storage.StorageArticles) 
                {
                    var articleresult = _storageArticleRepository.GetById(article.Id, cancellationToken);
                    if (articleresult is not null)
                    {
                        await _storageArticleRepository.Update(article, cancellationToken);
                        continue;
                    }
                    await _storageArticleRepository.Add(article, cancellationToken);

                }
            }

            var result = await Connection.ExecuteAsync("Update [dbo].[Storage] Set Name = @Name,Description = @Description Where Id = @Id",
                            new { Name = storage.Name, Description = storage.Description, Id = storage.Id });

            if (result < 1)
            {
                throw new StorageUpdateFailedInfastructureException($"Storage with ID: {storage.Id} not found");
            }
        }
    }
}
