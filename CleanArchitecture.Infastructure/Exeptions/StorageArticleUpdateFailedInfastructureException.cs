namespace CleanArchitecture.Infastructure.Exeptions
{
    internal class StorageArticleUpdateFailedInfastructureException : Exception
    {
        public StorageArticleUpdateFailedInfastructureException(string? message) : base(message)
        {
        }
    }
}
