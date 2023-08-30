using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using MediatR;

namespace CleanArchitecture.Application.ArticleUseCases.Commands.UpdateArticleName
{
    public record UpdateArticleNameCommand () : IRequest<IResult<StorageArticleDTO>>
    {
        public Guid ArticleID { get; set; }
        public string ArticleName { get; set; } = string.Empty;
    }

    public class UpdateArticleName : IRequestHandler<UpdateArticleNameCommand, IResult<StorageArticleDTO>>
    {
        private 
        Task<IResult<StorageArticleDTO>> IRequestHandler<UpdateArticleNameCommand, IResult<StorageArticleDTO>>.Handle(UpdateArticleNameCommand request, CancellationToken cancellationToken)
        {
            
        }
    }
}