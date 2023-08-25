using CleanArchitecture.Application.Exceptions;

namespace CleanArchitecture.Application.Common
{
    class TResult<T> : IResult<T>
    {

        internal TResult()
        {

        }
        public bool Success { get; set; }
        public T? Result { get; set; }
        public IApplicationException? Error { get; set; }


        public static TResult<T> OnError(IApplicationException ex)
        {
            var result = new TResult<T>();
            result.Success = false;
            result.Error = ex;
            return result;

        }

        public static TResult<T> OnSuccess(T obj)
        {
            var result = new TResult<T>();
            result.Success = true;
            result.Result = obj;
            return result;

        }



    }
}
