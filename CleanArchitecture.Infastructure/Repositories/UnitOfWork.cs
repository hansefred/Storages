using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infastructure.Common;
using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    public class UnitOfWork : IUnitofWork, IDisposable
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _transaction;

        private IStorageRepository? _storageRepository;
        private IStorageArticleRepository? _articleRepository;

        public UnitOfWork(IDBConnectionFactory dBConnectionFactory)
        {
            _dbConnection = dBConnectionFactory.GetDBConnection();
            _dbConnection.Open();
            _transaction = _dbConnection.BeginTransaction();
        }

        public IStorageRepository StorageRepository { get { return _storageRepository ?? (_storageRepository = new StorageRepository(_transaction, new StorageArticleRepository(_transaction))); } }

        public IStorageArticleRepository ArticleRepository { get { return _articleRepository ?? (_articleRepository = new StorageArticleRepository(_transaction)); } }

        /// <summary>
        /// Comit changes for underlying Repositories
        /// </summary>
        public void Commit()
        {
            try
            {
                _transaction.Commit();


            }
            catch 
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _dbConnection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _storageRepository = null;
            _articleRepository = null;
        }

        public void Dispose()
        {
            _transaction.Connection?.Close();
            _transaction.Connection?.Dispose();
            _transaction.Dispose();
        }
    }
}
