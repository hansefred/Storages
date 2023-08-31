namespace CleanArchitecture.Infastructure.Exeptions
{
    internal class StorageArticleCreateFailedInfastructureException : Exception
    {
        public StorageArticleCreateFailedInfastructureException(string? message) : base(message)
        {
        }
    }
}
