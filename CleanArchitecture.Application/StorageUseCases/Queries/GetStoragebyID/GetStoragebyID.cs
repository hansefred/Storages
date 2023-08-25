using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.StorageUseCases.Queries.GetStorages;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.StorageUseCases.Queries.GetStoragebyID
{
    public record GetStorageByIdQuery : IRequest<IResult<StorageDto>>
    {
        public Guid Id { get; set; }
    }

    internal class GetStorageById : IRequestHandler<GetStorageByIdQuery, IResult<StorageDto>>
    {
        public readonly IStorageRepository storageRepository;

        public GetStorageById(IStorageRepository storageRepository)
        {
            this.storageRepository = storageRepository;
        }

        public async Task<IEnumerable<StorageDto>> Handle(GetStoragesQuery request, CancellationToken cancellationToken)
        {
            List<StorageDto> storageDtos = new List<StorageDto>();
            foreach (var storage in await storageRepository.GetAll(cancellationToken))
            {
                storageDtos.Add(new StorageDto(storage));
            }

            return storageDtos;
        }


        async Task<IResult<StorageDto>> IRequestHandler<GetStorageByIdQuery, IResult<StorageDto>>.Handle(GetStorageByIdQuery request, CancellationToken cancellationToken)
        {

            var storage = await storageRepository.GetById(request.Id, cancellationToken);

            if (storage is not null)
            {
                return TResult<StorageDto>.OnSuccess(new StorageDto(storage));
            }

            return TResult<StorageDto>.OnError(new StorageNotFoundException($"Cannot find Storage with Id: {request.Id}"));


        }
    }
}
