using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories
{
    public interface IStorageRepository : IRepository<Storage>
    {
        /// <summary>
        /// Get Storage Repository and all related Storage Articles 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Storage?> GetById(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all Storage Repository and all related Storage Articles 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Storage>> GetAll( CancellationToken cancellationToken = default);
        /// <summary>
        /// Update Storage Object and all related Storage Articles
        /// </summary>
        /// <param name="storage">Storage Object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Update (Storage storage, CancellationToken cancellationToken = default);
        /// <summary>
        /// Add new Storage Object and all related Storage Articles
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Add (Storage entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Delete Storage Object and all related Storage Articles
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Delete (Storage entity, CancellationToken cancellationToken = default);
    }
}
