using FluentValidation;

namespace CleanArchitecture.Application.StorageUseCases.Commands.AddStorageArticletoStorage
{
    internal class AddStorageArticletoStorageValidator : AbstractValidator<AddStorageArticletoStorageCommand>
    {
        public AddStorageArticletoStorageValidator()
        {
            RuleFor(o => o.ArticleDescription).NotEmpty();
            RuleFor(o => o.ArticleName).NotEmpty();
            RuleFor(o => o.StorageID).NotEmpty();
        }
    }
}
