using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories
{
    public interface IStorageRepository : IRepository<Storage>
    {
        Task<Storage?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<List<Storage>> GetAll( CancellationToken cancellationToken = default);
        Task Add (Storage entity, CancellationToken cancellationToken = default);
        Task Delete (Storage entity, CancellationToken cancellationToken = default);
    }
}
