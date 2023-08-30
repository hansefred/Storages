using FluentValidation;

namespace CleanArchitecture.Application.ArticleUseCases.Commands.UpdateArticleName
{
    internal class UpdateArticleNameValidator : AbstractValidator<UpdateArticleNameCommand>
    {
        public UpdateArticleNameValidator()
        {
            RuleFor(x => x.ArticleName).NotEmpty();
            RuleFor(x => x.ArticleID).NotEmpty();
        }
    }
}
