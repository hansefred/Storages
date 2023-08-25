using FluentValidation;

namespace CleanArchitecture.Application.StorageUseCases.Queries.GetStoragebyID
{
    internal class GetStoragebyIDValidator : AbstractValidator<GetStorageByIdQuery>
    {
        public GetStoragebyIDValidator()
        {
            RuleFor(o => o.Id).NotEmpty();
        }
    }
}
