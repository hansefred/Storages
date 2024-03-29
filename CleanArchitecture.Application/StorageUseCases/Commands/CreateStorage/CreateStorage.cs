﻿using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTO;
using CleanArchitecture.Application.Exceptions;

namespace CleanArchitecture.Application.StorageUseCases.Commands.CreateStorage
{
    public record CreateStorageCommand : IRequest<IResult<StorageDto>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    internal class CreateStorage : RequestCommandHandlerBase, IRequestHandler<CreateStorageCommand, IResult<StorageDto>>
    {
        public CreateStorage(IUnitofWork unitofWork) : base(unitofWork)
        {
        }

        public async  Task<IResult<StorageDto>> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
        {
            var result = Storage.Create(Guid.NewGuid(), request.Name!, request.Description!);
            //Execute Handle
            if (result.IsSuccess)
            {
                var storage = result.Result!;
                try
                {
                    await _unitofWork.StorageRepository.Add(storage, cancellationToken);
                    _unitofWork.Commit();
                }
                catch (Exception ex)
                {
                    TResult<StorageDto>.OnError(new StorageApplicationErrorException(ex));
                }

            }
            //Handle Domain Errors
            return TResult<StorageDto>.OnError(new StorageApplicationErrorException(result!.DomainException!.ErrorMessage));

        }
    }
}
