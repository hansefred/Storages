namespace CleanArchitecture.Application.Common
{
    class TResult<T> : IResult<T>
    {

        internal TResult()
        {

        }
        public bool Success { get; set; }
        public T? Result { get; set; }
        public string? Error { get; set; }

        public static TResult<T> OnError(string message)
        {
            var result = new TResult<T>();
            result.Success = false;
            result.Error = message;
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
