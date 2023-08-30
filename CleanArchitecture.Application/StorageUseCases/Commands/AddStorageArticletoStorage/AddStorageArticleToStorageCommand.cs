using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.StorageUseCases.Commands.AddStorageArticleToStorage
{
    public record AddStorageArticleToStorageCommand(Guid StorageID, string ArticleName, string ArticleDescription) : IRequest<IResult<StorageDto>>;
    internal class AddStorageArticleToStorage : RequestCommandHandlerBase, IRequestHandler<AddStorageArticleToStorageCommand, IResult<StorageDto>>
    {
        public AddStorageArticleToStorage(IUnitofWork unitofWork) : base(unitofWork)
        {
        }

        public async Task<IResult<StorageDto>> Handle(AddStorageArticleToStorageCommand request, CancellationToken cancellationToken)
        {
            var storage = await _unitofWork.StorageRepository.GetById(request.StorageID, cancellationToken);
            if (storage is not null)
            {
                return TResult<StorageDto>.OnError(new StorageNotFoundException($"No Entity found with ID: {request.StorageID}"));
            }

            try
            {
                storage!.AddArticleToStorage(request.ArticleName, request.ArticleDescription);
                _unitofWork.Commit();
                storage = await _unitofWork.StorageRepository.GetById(request.StorageID, cancellationToken);
                return TResult<StorageDto>.OnSuccess(new StorageDto(storage!));
            }
            catch (Exception ex)
            {
                return TResult<StorageDto>.OnError(new StorageApplicationErrorException(ex));
            }


        }
    }
}
