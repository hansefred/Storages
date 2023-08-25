namespace CleanArchitecture.Domain.Primitives
{
    public interface ITResult<Entity> where Entity : CleanArchitecture.Domain.Entity
    {
        DomainException? DomainException { get; set; }
        bool IsSuccess { get; set; }
        Entity? Result { get; set; }
    }
}