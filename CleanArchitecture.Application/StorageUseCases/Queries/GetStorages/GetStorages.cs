using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.StorageUseCases.Queries.GetStorages
{
    public record GetStoragesQuery : IRequest<IEnumerable<StorageDto>>;
    internal class GetStorages : RequestQueryHandlerBase, IRequestHandler<GetStoragesQuery, IEnumerable<StorageDto>>
    {
        public GetStorages(IStorageRepository storageRepository) : base(storageRepository)
        {
        }

        public async Task<IEnumerable<StorageDto>> Handle(GetStoragesQuery request, CancellationToken cancellationToken)
        {
            List<StorageDto> storageDtos = new List<StorageDto>();
            foreach (var storage in await _storageRepository.GetAll(cancellationToken))
            {
                storageDtos.Add(new StorageDto(storage));
            }

            return storageDtos;
        }
    }
}
