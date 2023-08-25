using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.StorageUseCases.Commands.AddStorageArticletoStorage
{
    public record AddStorageArticletoStorageCommand(Guid StorageID, string ArticleName, string ArticleDescription) : IRequest<IResult<StorageDto>>;
    internal class AddStorageArticletoStorage : IRequestHandler<AddStorageArticletoStorageCommand, IResult<StorageDto>>
    {
        private readonly IUnitofWork _unitofWork;

        public AddStorageArticletoStorage(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<IResult<StorageDto>> Handle(AddStorageArticletoStorageCommand request, CancellationToken cancellationToken)
        {
            var storage = await _unitofWork.StorageRepository.GetById(request.StorageID);
            if (storage is not null)
            {
                return TResult<StorageDto>.OnError(new StorageNotFoundException($"No Entity found with ID: {request.StorageID}"));
            }

            try
            {
                storage!.AddArticleToStorage(request.ArticleName, request.ArticleDescription);
                _unitofWork.Commit();
                storage = await _unitofWork.StorageRepository.GetById(request.StorageID);
                return TResult<StorageDto>.OnSuccess(new StorageDto(storage!));
            }
            catch (Exception ex)
            {
                return TResult<StorageDto>.OnError(new StorageApplicationErrorException(ex));
            }


        }
    }
}
