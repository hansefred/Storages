using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.DTO
{
    internal class StorageArticleDTO
    {
        public StorageArticleDTO(StorageArticle storageArticle)
        {
            ID = storageArticle.Id;
            Name = storageArticle.ArticleName;
        }

        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;



    }
}
