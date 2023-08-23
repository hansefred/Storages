namespace CleanArchitecture.Domain.Repositories
{
    public interface IUnitofWork 
    {
        void Commit();
    }
}
