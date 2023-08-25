using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Common;

namespace CleanArchitecture.Application.StorageUseCases.Commands.CreateStorage
{
    public record CreateStorageCommand : IRequest<IResult<StorageDto>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    internal class CreateStorage : IRequestHandler<CreateStorageCommand, IResult<StorageDto>>
    {
        private readonly IUnitofWork unitofWork;

        public CreateStorage(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }

        public async  Task<IResult<StorageDto>> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
        {
           if (ValidatorHelper.Validate<CreateStorageCommandValidator, CreateStorageCommand,StorageDto>(request, out var ValidationErrorResult))
            {
                return ValidationErrorResult!;
            }

            var oneOf = Storage.Create(Guid.NewGuid(), request.Name!, request.Description!);

            //Execute Handle
            if (oneOf.IsT0)
            {
                var storage = oneOf.AsT0;
                try
                {
                    await unitofWork.StorageRepository.Add(storage);
                    unitofWork.Commit();
                }
                catch (Exception ex)
                {
                    TResult<StorageDto>.OnError(ex.Message);
                }

            }

            //Handle Domain Errors
            return OneOfHelper.HandleError<StorageDto>(oneOf);

        }
    }
}
