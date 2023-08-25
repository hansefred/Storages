using OneOf;

namespace CleanArchitecture.Application.Common
{
    public static class OneOfHelper
    {
        public static IResult<T> HandleError<T> (IOneOf oneOf)
        {
            var ex = (oneOf.Value as Exception);
            if (ex is not null)
            {
                return TResult<T>.OnError(ex.Message);
            }
            return TResult<T>.OnError("");
        }
    }
}
