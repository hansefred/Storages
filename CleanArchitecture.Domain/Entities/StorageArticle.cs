using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Domain.Entities;

public class StorageArticle : Entity
{
    internal StorageArticle(Guid id, string articleName, string description) : this (id,null,articleName,description)
    {
    }

    internal StorageArticle(Guid id, Storage? storage, string articleName, string description) : base(id)
    {
        ArticleName = articleName;
        Description = description;
        Storage = storage;
    }

    public Storage? Storage { get; internal set; }
    public string ArticleName { get; private set; }
    public string Description { get; private set; }

    /// <summary>
    /// Create a new Entity of Storage Article
    /// </summary>
    /// <param name="id">ID of new Entity</param>
    /// <param name="articleName">Aricle Name of new Entity</param>
    /// <param name="description">Description of new Entity</param>
    /// <returns>Returns a ITResult, contains error or new entity</returns>
    internal static ITResult<StorageArticle> Create (Guid id, string articleName, string description)
    {
      if (articleName is null || articleName.Length < 5 || articleName.Length > 49)
      {
            return TResult<StorageArticle>.OnError(new StorageArticleNameIsOutofRangeDomainException("Article Name must be in range from 5 - 49"));
      }
      if (description.Length > 149)
      {
            return TResult<StorageArticle>.OnError(new StorageArticleDescriptionIsOutofRangeDomainException("Article Description can be longer than 149 Chars"));
      }

        return TResult<StorageArticle>.OnSuccess(new StorageArticle(id, articleName, description));

    }



}
