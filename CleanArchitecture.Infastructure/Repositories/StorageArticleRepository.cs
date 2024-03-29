﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infastructure.Exeptions;
using Dapper;
using System.Data;

namespace CleanArchitecture.Infrastructure.Repositories
{
    internal class StorageArticleRepository : BaseRepository, IStorageArticleRepository
    {
        public StorageArticleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        /// <summary>
        /// Add new Storage Article, Article must be added to DB before, please call Commit after to persist 
        /// </summary>
        /// <param name="storageArticle">Article Object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task Add(StorageArticle storageArticle, CancellationToken cancellationToken = default)
        {
            await Connection.ExecuteAsync("INSERT INTO [dbo].[StorageArticle] (Id,Name, Description,Storage_Id)VALUES (@Id, @Name, @Description,@StorageId)",
                             new { Id = storageArticle.Id,Name = storageArticle.ArticleName, Description = storageArticle.Description , StorageId  = storageArticle!.Storage!.Id}, transaction: Transaction);

        }

        /// <summary>
        /// Update Storage Article, please call Commit after to persist 
        /// </summary>
        /// <param name="storageArticle"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="StorageArticleUpdateFailedInfastructureException"></exception>
        public async Task Update(StorageArticle storageArticle, CancellationToken cancellationToken = default)
        {
            var result = await Connection.ExecuteAsync("Update [dbo].[StorageArticle] Set Name = @Name, Description = @Description, Storage_Id = @StorageId Where Id = @Id",
                                    new { Name = storageArticle.ArticleName, Description = storageArticle.Description, Id = storageArticle.Id, StorageId = storageArticle!.Storage!.Id }, transaction: Transaction);

            if (result < 1)
            {
                throw new StorageArticleUpdateFailedInfastructureException($"StorageArticle with ID: {storageArticle.Id} not found");
            }
        }

        /// <summary>
        /// Get Storage Article by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<StorageArticle?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await Connection.QueryAsync<StorageArticle>("Select Id, Name, Description FROM [dbo].[StorageArticle] Where Id = @Id",
                        new { Id = id });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Remove Storage Article, please call Commit after to persist 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Remove (Guid id, CancellationToken cancellationToken= default)
        {
            await Connection.ExecuteAsync("Delete [dbo].[StorageArticle] Where Id = @Id", new { Id = id },  transaction: Transaction);
        }


    }
}
