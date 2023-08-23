namespace CleanArchitecture.Domain;

public class StorageArticleNameIsOutofRangeDomainException : DomainException
{
    public StorageArticleNameIsOutofRangeDomainException(string message) : base(message)
    {
    }
}
