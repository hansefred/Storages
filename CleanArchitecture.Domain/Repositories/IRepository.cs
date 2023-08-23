using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Domain.Repositories
{
    public interface IRepository<T>
        where T : AggregateRoot
    {
    }
}
