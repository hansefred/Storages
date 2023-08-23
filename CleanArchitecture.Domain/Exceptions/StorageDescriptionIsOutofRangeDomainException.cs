namespace CleanArchitecture.Domain;

public class StorageDescriptionIsOutofRangeDomainException : DomainException
{
    public StorageDescriptionIsOutofRangeDomainException(string message) : base(message)
    {
    }
}
