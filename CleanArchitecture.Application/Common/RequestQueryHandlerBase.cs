using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Application.Common
{
    abstract class RequestQueryHandlerBase
    {
        internal readonly IStorageRepository _storageRepository;

        public RequestQueryHandlerBase(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }
    }
}
