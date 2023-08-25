namespace CleanArchitecture.Application.Exceptions
{
    public class StorageApplicationErrorException : ApplicationException
    {
        public StorageApplicationErrorException(string errorMessage) : base(errorMessage) { }
        
        public StorageApplicationErrorException (Exception ex ) : base(ex) { }
    }
}
