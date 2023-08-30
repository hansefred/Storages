namespace CleanArchitecture.Application.Exceptions
{
    internal class StorageArticleApplicationException : ApplicationException
    {
        public StorageArticleApplicationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
