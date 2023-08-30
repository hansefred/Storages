using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using Dapper;
using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    internal class StorageRepository : BaseRepository, IStorageRepository
    {


        public StorageRepository(IDbTransaction dbTransaction) : base(dbTransaction)
        {

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
            await Connection.ExecuteAsync("Update [dbo].[Storage] Set Name = @Name,Description = @Description Where Id = @Id",
                                        new { Name = storage.Name, Description = storage.Description, Id = storage.Id });

            if (storage.StorageArticles.Any()) 
            {
                foreach (var article in storage.StorageArticles) 
                {

                }
            }
        }
    }
}
