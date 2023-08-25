using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.DTO
{
    internal class StorageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public StorageDto(Storage storage)
        {
            Id = storage.Id;
            Name = storage.Name;
            Description = storage.Description;
        }

    }
}
