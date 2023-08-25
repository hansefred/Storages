using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.StorageUseCases.Queries
{
    public record GetStoragesQuery: IRequest<IEnumerable<StorageDto>>;
    internal class GetStorages : IRequestHandler<GetStoragesQuery, IEnumerable<StorageDto>>
    {
        public readonly IStorageRepository storageRepository;

        public GetStorages(IStorageRepository storageRepository)
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
    }
}
