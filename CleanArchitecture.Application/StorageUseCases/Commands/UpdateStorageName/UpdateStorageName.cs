using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.StorageUseCases.Commands.UpdateStorageName
{
    public record UpdateStorageNameCommand () : IRequest<IResult<StorageDto>>
    {
        public Guid StorageID { get; set; }
        public string StorageName { get; set; } = string.Empty;
    }
    public class UpdateStorageName : RequestCommandHandlerBase, IRequestHandler<UpdateStorageNameCommand, IResult<StorageDto>>
    {
        public UpdateStorageName(IUnitofWork unitofWork) : base(unitofWork)
        {
        }

        async Task<IResult<StorageDto>> IRequestHandler<UpdateStorageNameCommand, IResult<StorageDto>>.Handle(UpdateStorageNameCommand request, CancellationToken cancellationToken)
        {
           var storage = await _unitofWork.StorageRepository.GetById(request.StorageID, cancellationToken);
            if (storage is null)
            {
                return TResult<StorageDto>.OnError(new StorageNotFoundException($"Storage with ID: {request.StorageID} not found"));
            }
            var result = storage.UpdateStorageName(request.StorageName);
            if (result.IsSuccess) 
            {
                _unitofWork.Commit();
                return TResult<StorageDto>.OnSuccess(new StorageDto(result.Result!));
            }
            return TResult<StorageDto>.OnError(new StorageApplicationErrorException(result.DomainException!.ToString() ?? ""));
        }


    }
}
