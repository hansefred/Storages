using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Application.Common
{
    public abstract class RequestCommandHandlerBase
    {
        internal readonly IUnitofWork _unitofWork;

        protected RequestCommandHandlerBase(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
    }
}
