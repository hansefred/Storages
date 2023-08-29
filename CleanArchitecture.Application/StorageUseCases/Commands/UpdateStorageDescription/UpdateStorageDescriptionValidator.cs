// Ignore Spelling: Validator

using FluentValidation;

namespace CleanArchitecture.Application.StorageUseCases.Commands.UpdateStorageDescription
{
    internal class UpdateStorageDescriptionValidator : AbstractValidator<UpdateStorageDescriptionCommand>
    {
        public UpdateStorageDescriptionValidator()
        {
            RuleFor(o => o.Description).MinimumLength(0).MaximumLength(149);
            RuleFor(o => o.StorageID).NotNull();
        }
    }
}
