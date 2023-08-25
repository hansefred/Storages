namespace CleanArchitecture.Application.Exceptions
{
    public interface IApplicationException
    {
        string ErrorMessage { get; set; }
    }
}