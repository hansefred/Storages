// Ignore Spelling: Validator

using FluentValidation;

namespace CleanArchitecture.Application.StorageUseCases.Commands.AddStorageArticleToStorage
{
    internal class AddStorageArticleToStorageValidator : AbstractValidator<AddStorageArticleToStorageCommand>
    {
        public AddStorageArticleToStorageValidator()
        {
            RuleFor(o => o.ArticleDescription).NotEmpty();
            RuleFor(o => o.ArticleName).NotEmpty();
            RuleFor(o => o.StorageID).NotEmpty();
        }
    }
}
