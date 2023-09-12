namespace CleanArchitecture.Domain.Exceptions
{
    internal class StorageArticleNotFoundDomainException : DomainException
    {
        public StorageArticleNotFoundDomainException(string message) : base(message)
        {
        }
    }
}
