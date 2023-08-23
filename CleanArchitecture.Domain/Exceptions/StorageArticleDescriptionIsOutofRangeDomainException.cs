namespace CleanArchitecture.Domain;

public class StorageArticleDescriptionIsOutofRangeDomainException : DomainException
{
    public StorageArticleDescriptionIsOutofRangeDomainException(string message) : base(message)
    {
    }
}
