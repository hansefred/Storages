using CleanArchitecture.Domain.Repositories;
using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    public class UnitOfWork : IUnitofWork, IDisposable
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _transaction;

        private IStorageRepository? _storageRepository;

        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
            _transaction = _dbConnection.BeginTransaction();
        }

        public IStorageRepository StorageRepository { get { return _storageRepository ?? (_storageRepository = new StorageRepository(_transaction)); } }



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

        }

        public void Dispose()
        {
            _transaction.Connection?.Close();
            _transaction.Connection?.Dispose();
            _transaction.Dispose();
        }
    }
}
