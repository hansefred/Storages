using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.ArticleUseCases.Commands.UpdateArticleDescription
{
    record class UpdateArticleDescriptionCommand () : IRequest<IResult<StorageArticleDTO>>
    {
        public Guid ArticleID { get; set; }
        public string ArticleDescription { get; set; } = string.Empty;
    }

    public class UpdateArticleDescription : RequestCommandHandlerBase, IRequestHandler<UpdateArticleDescriptionCommand, IResult<StorageArticleDTO>>
    {
        public UpdateArticleDescription(IUnitofWork unitofWork) : base(unitofWork)
        {
        }

        async Task<IResult<StorageArticleDTO>> IRequestHandler<UpdateArticleDescriptionCommand, IResult<StorageArticleDTO>>.Handle(UpdateArticleDescriptionCommand request, CancellationToken cancellationToken)
        {
            var article = await _unitofWork.ArticleRepository.GetById(request.ArticleID, cancellationToken);
            if (article is null)
            {
                return TResult<StorageArticleDTO>.OnError(new StorageArticleNotFoundException($"Storage Article with ID: {request.ArticleID} not found"));
            }

            var result = article.UpdateStorageArticleDescription(request.ArticleDescription);    
            if (result.IsSuccess) 
            {
                return TResult<StorageArticleDTO>.OnSuccess(new StorageArticleDTO(result.Result!));
            }
            return TResult<StorageArticleDTO>.OnError(new StorageArticleApplicationException(result.DomainException!.ToString() ?? ""));
        }
    }
}
