using CleanArchitecture.Domain;
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

        public Task Add(Storage entity, CancellationToken cancellationToken = default)
        {
            Connection.ExecuteScalarAsync("", transaction: Transaction);
        }

        public Task Delete(Storage entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Storage>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Storage?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
