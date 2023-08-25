namespace CleanArchitecture.Domain;

public abstract class DomainException
{
    public string ErrorMessage { get; set; }
    protected DomainException(string message)
    {
       ErrorMessage = message;
    }
    protected DomainException(Exception ex)
    {
        ErrorMessage = ex.ToString();
    }
}
