namespace CleanArchitecture.Domain;

public sealed class StorageNameIsOutofRangeDomainException : DomainException
{
    public StorageNameIsOutofRangeDomainException(string message) : base(message)
    {
    }
}
