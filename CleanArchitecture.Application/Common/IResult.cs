namespace CleanArchitecture.Application.Common
{
    public interface IResult<T>
    {
        string? Error { get; set; }
        T? Result { get; set; }
        bool Success { get; set; }
    }
}