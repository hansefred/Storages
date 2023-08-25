using FluentValidation;

namespace CleanArchitecture.Application.Common
{
    static class ValidatorHelper
    {
        public static bool Validate<V,R,T>(R request, out IResult<T>? validationErrorResult)
            where V : AbstractValidator<R>, new ()
        {
            V validator = new();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                string errormessage = "";
                foreach (var error in result.Errors)
                {
                    errormessage = errormessage + " " + error.ErrorMessage;
                }
                validationErrorResult = TResult<T>.OnError(errormessage);
                return false;
            }

            validationErrorResult = null;
            return true;
        }
    }
}
