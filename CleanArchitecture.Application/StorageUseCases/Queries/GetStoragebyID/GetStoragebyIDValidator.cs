// Ignore Spelling: Validator

using FluentValidation;

namespace CleanArchitecture.Application.StorageUseCases.Queries.GetStorageByID
{
    internal class GetStorageByIDValidator : AbstractValidator<GetStorageByIdQuery>
    {
        public GetStorageByIDValidator()
        {
            RuleFor(o => o.Id).NotEmpty();
        }
    }
}
