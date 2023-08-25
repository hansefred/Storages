using OneOf;

namespace CleanArchitecture.Domain.Entities;

public class StorageArticle : Entity
{
    internal StorageArticle (Guid id, Storage storage, string articleName, string description ) : base (id)
    {
        ArticleName = articleName;
        Description = description;
        Storage = storage;
    }

    public Storage Storage { get; private set; }
    public string ArticleName { get; private set; }
    public string Description { get; private set; }

    internal OneOf<StorageArticle, StorageArticleNameIsOutofRangeDomainException,StorageArticleDescriptionIsOutofRangeDomainException> Create (Guid id, Storage storage, string articleName, string description)
    {
      if (articleName is null || articleName.Length < 5 || articleName.Length > 49)
      {
        return new StorageArticleNameIsOutofRangeDomainException ("Article Name must be in range from 5 - 49");
      }
      if (description.Length > 149)
      {
        return new StorageArticleDescriptionIsOutofRangeDomainException ("Article Description can be longer than 149 Chars");
      }

      return new StorageArticle(id, storage, articleName, description);

    }

}
