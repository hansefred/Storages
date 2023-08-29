using CleanArchitecture.Domain.Primitives;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CleanArchitecture.Domain.Entities;


public class Storage : AggregateRoot
{

    private Storage(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
        _articles = new List<StorageArticle>();
    }

    private List<StorageArticle> _articles;

    public List<StorageArticle> StorageArticles => _articles;
    public string Name { get; private set;}
    public string Description { get; private set; }

    #region Private Method

    private static bool ValidateName(string name, out ITResult<Storage>? OnError)
    {
        if (name is null || name.Length < 5 || name.Length > 49)
        {
            OnError =  TResult<Storage>.OnError(new StorageNameIsOutofRangeDomainException("Storage Name must be between 5 and 49 Chars"));
            return false;
        }
        OnError = null;
        return true;
    }

    private static bool ValidateDescription(string description, out ITResult<Storage>? OnError)
    {
        if (description is null || description.Length > 149)
        {
            OnError =  TResult<Storage>.OnError(new StorageDescriptionIsOutofRangeDomainException("Storage Description can't be longer than 149 Chars"));
            return false;
        }
        OnError = null;
        return true;
    }
    #endregion


    /// <summary>
    /// Create a new Entity from Storage
    /// </summary>
    /// <param name="id">Unique ID of Entity</param>
    /// <param name="name">Name of the Storage, must be between 5 and 49 chars</param>
    /// <param name="description">New Value for Description could not be bigger than 149 chars</param>
    /// <returns>Returns a <see cref="ITResult{Entity}"/> from Type <see cref="Storage"/>, contains error or new entity, check Success Property</returns>
    public static ITResult<Storage> Create (Guid id, string name, string description)
    {
       
        if (!ValidateName(name,out ITResult<Storage>? OnErrorName))
        {
            return OnErrorName!;
        }

       if (!ValidateDescription(description, out ITResult<Storage>? OnErrorDescription))
        {
            return OnErrorDescription!;
        }


       return TResult<Storage>.OnSuccess(new Storage(id, name, description));
    }


    /// <summary>
    /// Create a article and add it to storage 
    /// </summary>
    /// <param name="articleName">Name of the New Article, must be between 5 and 49 chars/param>
    /// <param name="articleDescription">New Value for Description could not be bigger than 149 chars</param>
    /// <returns>Returns a <see cref="ITResult{Entity}"/> from Type <see cref="Storage"/>, contains error or entity, check Success Property</returns>
    public ITResult<Storage> AddArticleToStorage(string articleName, string articleDescription)
    {
        var result = StorageArticle.Create(Guid.NewGuid(), articleName, articleDescription);
        if (result.IsSuccess)
        {
            var article = result.Result!;
            _articles.Add(article);
            article.Storage = this;

            return TResult<Storage>.OnSuccess(this);
        }

        return TResult<Storage>.OnError(result.DomainException!);
    }

    
    /// <summary>
    /// Update Description of existing Storage
    /// </summary>
    /// <param name="description">New Value for Description could not be bigger than 149 chars</param>
    /// <returns>A <see cref="ITResult{Entity}"/> from type <see cref="Storage"/> Property Success need to be checked </returns>
    public ITResult<Storage> UpdateStorageDescription (string  description) 
    {
        if (ValidateDescription(description, out ITResult<Storage>? OnError))
        {
            return OnError!;
        }
        Description = description;
        return TResult<Storage>.OnSuccess(this);
    }

    /// <summary>
    /// Update Name of existing Storage
    /// </summary>
    /// <param name="description">New Value for Description could not be bigger than 149 chars</param>
    /// <returns>A <see cref="ITResult{Entity}"/> from type <see cref="Storage"/> Property Success need to be checked </returns>
    public ITResult<Storage> UpdateStorageName(string name)
    {
        if (ValidateName(name, out ITResult<Storage>? OnError))
        {
            return OnError!;
        }
        Name = name;
        return TResult<Storage>.OnSuccess(this);
    }
}


    
