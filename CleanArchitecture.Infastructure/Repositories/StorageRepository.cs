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

        /// <summary>
        /// Add a New Storage Entity and all related Storage Article to the Transaction, please call Commit after to persist
        /// </summary>
        /// <param name="entity">The new Entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async  Task Add(Storage entity, CancellationToken cancellationToken = default)
        {
            await Connection.ExecuteAsync("INSERT INTO [dbo].[Storage] (Id,Name, Description)VALUES (@Id, @Name, @Description)",
                                    new { Id = entity.Id, Name = entity.Name, Description = entity.Description }, transaction: Transaction);

            foreach (var obj in entity.StorageArticles)
            {
                await _storageArticleRepository.Add(obj);
            }


        }

        /// <summary>
        /// Delete Storage with all related Articles, please call Commit after to persist
        /// </summary>
        /// <param name="id">The ID of the Storage</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Delete(Storage entity, CancellationToken cancellationToken = default)
        {
            var storage = await GetById(entity.Id);
            if (storage is not null)
            {
                foreach (var article in storage.StorageArticles)
                {
                    await _storageArticleRepository.Remove(article.Id);
                }
                await Connection.ExecuteAsync("DELETE [dbo].[Storage] Where Id = @Id", new { Id = entity.Id },
                                transaction: Transaction);
            }
        }

        /// <summary>
        /// Returns a List of all Storage Object with related Storage Article
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Storage>> GetAll(CancellationToken cancellationToken = default)
        {

            var storageDictionary = new Dictionary<Guid, Storage>();
            var result = await Connection.QueryAsync<Storage, StorageArticle, Storage>("SELECT Storage.Id, Storage.Name, Storage.Description, StorageArticle.Id AS Split,StorageArticle.Name AS ArticleName,StorageArticle.Description FROM [TestDB].[dbo].[Storage] AS Storage LEFT JOIN [TestDB].[dbo].[StorageArticle] AS StorageArticle ON Storage.Id = StorageArticle.Storage_Id",
                                                               (s, a) =>
                                                               {
                                                                   Storage? storageQuery;
                                                                   if (!storageDictionary.TryGetValue(s.Id, out storageQuery))
                                                                   {
                                                                       storageQuery = s;
                                                                       storageDictionary.Add(storageQuery.Id, storageQuery);

                                                                   }
                                                                   if (a is not null)
                                                                   {
                                                                       storageQuery.AddArticleToStorage(a.Id, a.ArticleName, a.Description);
                                                                   }


                                                                   return storageQuery;
                                                               }, splitOn: "Split", transaction: Transaction);


            if (result is null) 
            {
                return new();
            }
            return result.ToList();
        }

        /// <summary>
        /// Returns a Storage Object with the specified ID
        /// </summary>
        /// <param name="id">ID of Storage Object</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object or null</returns>
        public async Task<Storage?> GetById(Guid id, CancellationToken cancellationToken = default)
        {

            var storageDictionary = new Dictionary<Guid, Storage>();
            var result = await Connection.QueryAsync<Storage, StorageArticle?, Storage>("SELECT Storage.Id, Storage.Name, Storage.Description, StorageArticle.Id AS Split,StorageArticle.Name AS ArticleName,StorageArticle.Description FROM [TestDB].[dbo].[Storage] AS Storage LEFT JOIN [TestDB].[dbo].[StorageArticle] AS StorageArticle ON Storage.Id = StorageArticle.Storage_Id Where Storage.Id = @Id",
                                                              (s, a) =>
                                                              {
                                                                  Storage? storageQuery;
                                                                  if (!storageDictionary.TryGetValue(s.Id, out storageQuery))
                                                                  {
                                                                      storageQuery = s;
                                                                      storageDictionary.Add(storageQuery.Id, storageQuery);

                                                                  }
                                                                  if (a.Id != Guid.Empty)
                                                                  {
                                                                      storageQuery.AddArticleToStorage(a.Id, a.ArticleName, a.Description);
                                                                  }


                                                                  return storageQuery;
                                                              }, splitOn: "Split", param: new { Id = id }, transaction: Transaction);
            

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Update Storage and all Storage Article, please call Commit after to persist
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="StorageNotFoundInfastructureException">No Article found</exception>
        /// <exception cref="StorageUpdateFailedInfastructureException"></exception>
        public async Task Update(Storage storage, CancellationToken cancellationToken = default)
        {
            var storageObject = await GetById(storage.Id);
            if (storageObject is null)
            {
                throw new StorageNotFoundInfastructureException($"No Storage Object found with ID: {storage.Id}");
            }

            //Delete Articles 
            foreach ( var obj in storageObject.StorageArticles)
            {
                var exists = storage.StorageArticles.FirstOrDefault(o => o.Id == obj.Id);
                if (exists is null)
                {
                    await _storageArticleRepository.Remove(obj.Id);
                }
            }

            //Add new Articles 
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
                            new { Name = storage.Name, Description = storage.Description, Id = storage.Id }, transaction: Transaction);


        }
    }
}
