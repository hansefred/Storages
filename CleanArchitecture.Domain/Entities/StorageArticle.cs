using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Domain.Entities;

public class StorageArticle : Entity
{
    private StorageArticle(): base(Guid.Empty)
    {
        ArticleName = "";
        Description = "";
    }
    private StorageArticle(Guid id, string articleName, string description) : this (id,null,articleName,description)
    {

    }

    private StorageArticle(Guid id, Storage? storage, string articleName, string description) : base(id)
    {
        ArticleName = articleName;
        Description = description;
        Storage = storage;
    }

    public Storage? Storage { get; internal set; }
    public string ArticleName { get; private set; }
    public string Description { get; private set; }


    #region private static Methode
    private static bool ValidateArticleName (string articleName, out ITResult<StorageArticle>? OnError)
    {
        if (articleName is null || articleName.Length < 5 || articleName.Length > 49)
        {
            OnError =  TResult<StorageArticle>.OnError(new StorageArticleNameIsOutofRangeDomainException("Article Name must be in range from 5 - 49"));
            return false;
        }
        OnError = null;
        return true;
    }

    private static bool ValidateArticleDescription(string articleDescription, out ITResult<StorageArticle>? OnError)
    {
        if (articleDescription.Length > 149)
        {
            OnError =  TResult<StorageArticle>.OnError(new StorageArticleDescriptionIsOutofRangeDomainException("Article Description can be longer than 149 Chars"));
            return false;
        }
        OnError = null;
        return true;
    }
    #endregion

    /// <summary>
    /// Create a new Entity of Storage Article
    /// </summary>
    /// <param name="id">ID of new Entity</param>
    /// <param name="articleName">Article Name of new Entity must be between 5 and 49 chars</param>
    /// <param name="description">Description of new Entity cant be longer than 149 chars</param>
    /// <returns>Returns a <see cref="ITResult{Entity}"/>, contains error or new entity, please check Success Property</returns>
    internal static ITResult<StorageArticle> Create (Guid id, string articleName, string description)
    {
        if (!ValidateArticleDescription(description, out ITResult<StorageArticle>? OnDescriptionError))
        {
            return OnDescriptionError!;
        }
        if (!ValidateArticleName(articleName, out ITResult<StorageArticle>? OnArticleError))
        {
            return OnArticleError!;
        }
        return TResult<StorageArticle>.OnSuccess(new StorageArticle(id, articleName, description));
    }


    /// <summary>
    /// Update StorageArticleName
    /// </summary>
    /// <param name="articleName">Article Name of new Entity must be between 5 and 49 chars</param>
    /// <returns>Returns a <see cref="ITResult{Entity}"/>, contains error or new entity, please check Success Property</returns>
    public ITResult<StorageArticle> UpdateStorageArticleName (string articleName)
    {
        if (!ValidateArticleName(articleName,out ITResult<StorageArticle>? OnArticleNameError))
        {
            return OnArticleNameError!;
        }
        ArticleName = articleName;
        return TResult<StorageArticle>.OnSuccess(this);
    }


    /// <summary>
    /// Update StorageArticleDescription
    /// </summary>
    /// <param name="articleDescription">Description of new Entity cant be longer than 149 chars</param>
    /// <returns>Returns a <see cref="ITResult{Entity}"/>, contains error or new entity, please check Success Property</returns>
    public ITResult<StorageArticle> UpdateStorageArticleDescription(string articleDescription)
    {
        if (!ValidateArticleDescription(articleDescription, out ITResult<StorageArticle>? OnArticleNameError))
        {
            return OnArticleNameError!;
        }
        Description = articleDescription;
        return TResult<StorageArticle>.OnSuccess(this);
    }



}
