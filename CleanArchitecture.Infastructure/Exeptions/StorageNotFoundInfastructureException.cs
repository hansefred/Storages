namespace CleanArchitecture.Infastructure.Exeptions
{
    internal class StorageNotFoundInfastructureException : Exception
    {
        public StorageNotFoundInfastructureException(string? message) : base(message)
        {
        }
    }
}
