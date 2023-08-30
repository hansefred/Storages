using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.ArticleUseCases.Commands.UpdateArticleName
{
    public record UpdateArticleNameCommand () : IRequest<IResult<StorageArticleDTO>>
    { 
        public Guid ArticleID { get; set; }
        public string ArticleName { get; set; } = string.Empty;
    }

    public class UpdateArticleName : RequestCommandHandlerBase, IRequestHandler<UpdateArticleNameCommand, IResult<StorageArticleDTO>>
    {
        public UpdateArticleName(IUnitofWork unitofWork) : base(unitofWork)
        {
        }

        async Task<IResult<StorageArticleDTO>> IRequestHandler<UpdateArticleNameCommand, IResult<StorageArticleDTO>>.Handle(UpdateArticleNameCommand request, CancellationToken cancellationToken)
        {
            var article = await _unitofWork.ArticleRepository.GetById(request.ArticleID);
            if (article is null)
            {
                return TResult<StorageArticleDTO>.OnError(new StorageArticleNotFoundException($"Storage Article with ID: {request.ArticleID} not found"));
            }

            var result = article.UpdateStorageArticleName(request.ArticleName);

            if (result.IsSuccess) 
            {
                return TResult<StorageArticleDTO>.OnSuccess(new StorageArticleDTO(result!.Result!));
            }
            return TResult<StorageArticleDTO>.OnError(new StorageArticleApplicationException(result!.DomainException!.ToString() ?? "");
        }
    }
}