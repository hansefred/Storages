// Ignore Spelling: Validator

using FluentValidation;

namespace CleanArchitecture.Application.StorageUseCases.Commands.CreateStorage
{
    internal class CreateStorageCommandValidator : AbstractValidator<CreateStorageCommand>
    {
        public CreateStorageCommandValidator()
        {
            RuleFor( o  => o.Name ).NotEmpty();
            RuleFor(o => o.Description).NotEmpty();
        }

    }
}
