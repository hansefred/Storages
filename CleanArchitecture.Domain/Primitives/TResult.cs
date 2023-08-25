namespace CleanArchitecture.Domain.Primitives
{
    public class TResult<Entity> : ITResult<Entity> where Entity : CleanArchitecture.Domain.Entity
    {
        internal TResult()
        {

        }

        public bool IsSuccess { get; set; }
        public Entity? Result { get; set; }
        public DomainException? DomainException { get; set; }

        internal static TResult<Entity> OnSuccess(Entity entity)
        {
            var result = new TResult<Entity>();
            result.Result = entity;
            result.IsSuccess = true;
            return result;
        }

        internal static TResult<Entity> OnError(DomainException exception)
        {
            var result = new TResult<Entity>();
            result.DomainException = exception;
            result.IsSuccess = false;
            return result;
        }
    }
}
