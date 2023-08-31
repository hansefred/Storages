namespace CleanArchitecture.Infastructure.Exeptions
{
    internal class StorageUpdateFailedInfastructureException : Exception
    {
        public StorageUpdateFailedInfastructureException(string? message) : base(message)
        {
        }
    }
}
