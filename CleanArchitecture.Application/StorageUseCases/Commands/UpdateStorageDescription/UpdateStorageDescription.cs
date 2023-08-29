using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.StorageUseCases.Commands.AddStorageArticleToStorage;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.StorageUseCases.Commands.UpdateStorageDescription
{
    public record UpdateStorageDescriptionCommand : IRequest<IResult<StorageDto>>
    {
        public Guid StorageID { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateStorageDescription : IRequestHandler<UpdateStorageDescriptionCommand, IResult<StorageDto>>
    {
        private readonly IUnitofWork _unitOfWork;

        public UpdateStorageDescription(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task<IResult<StorageDto>> IRequestHandler<UpdateStorageDescriptionCommand, IResult<StorageDto>>.Handle(UpdateStorageDescriptionCommand request, CancellationToken cancellationToken)
        {
            var storage = await _unitOfWork.StorageRepository.GetById(request.StorageID, cancellationToken);
            if (storage is null)
            {
                return TResult<StorageDto>.OnError(new StorageNotFoundException($"Cannot find Storage with ID {request.StorageID}"));
            }

            var result = storage.UpdateStorageDescription(request.Description);
            if (result.IsSuccess) 
            {
                _unitOfWork.Commit();
                return TResult<StorageDto>.OnSuccess(new StorageDto(result.Result!));
            }
            return TResult<StorageDto>.OnError(new StorageApplicationErrorException(result!.DomainException!.ToString() ?? ""));
        }

       
    }
}
