namespace CleanArchitecture.Application.Exceptions
{
    internal class StorageArticleNotFoundException : ApplicationException
    {
        public StorageArticleNotFoundException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
