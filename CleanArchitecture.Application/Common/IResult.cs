using CleanArchitecture.Application.Exceptions;

namespace CleanArchitecture.Application.Common
{
    public interface IResult<T>
    {
        IApplicationException? Error { get; set; }
        T? Result { get; set; }
        bool Success { get; set; }
    }
}