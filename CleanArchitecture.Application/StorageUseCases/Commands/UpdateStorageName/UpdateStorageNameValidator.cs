using FluentValidation;

namespace CleanArchitecture.Application.StorageUseCases.Commands.UpdateStorageName
{
    public class UpdateStorageNameValidator : AbstractValidator<UpdateStorageNameCommand>
    {
        public UpdateStorageNameValidator()
        {
            RuleFor (o => o.StorageID).NotEmpty();
            RuleFor(o => o.StorageName).NotEmpty();
        }
    }
}
