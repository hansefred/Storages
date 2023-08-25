namespace CleanArchitecture.Application.Exceptions
{
    public class StorageNotFoundException : ApplicationException
    {
        public StorageNotFoundException(string errorMessage): base(errorMessage) 
        {
            
        }
    }
}
