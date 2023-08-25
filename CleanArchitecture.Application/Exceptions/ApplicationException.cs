namespace CleanArchitecture.Application.Exceptions
{
    public abstract class ApplicationException : IApplicationException
    {
        internal ApplicationException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        internal ApplicationException(Exception ex)
        {
            ErrorMessage = ex.ToString();
        }
        public string ErrorMessage { get; set; }
    }
}
