using CleanArchitecture.Domain.Primitives;


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
    public string Description { get; private set;}

    /// <summary>
    /// Create a new Entity from Storage
    /// </summary>
    /// <param name="id">ID of Entity</param>
    /// <param name="name">Name of the Storage</param>
    /// <param name="description">Description of the Storage</param>
    /// <returns>Returns a ITResult, contains Error or new Entity </returns>
    public static ITResult<Storage> Create (Guid id, string name, string description)
    {
       if (name is null || name.Length < 5 || name.Length > 49)
       {
            return TResult<Storage>.OnError(new StorageNameIsOutofRangeDomainException("Storage Name must be betwenn 5 and 49 Chars"));
       }
       if (description is null || description.Length > 149)
       {
            return TResult<Storage>.OnError(new StorageDescriptionIsOutofRangeDomainException("Storage Description can't be longer than 149 Chars"));
       }

       return TResult<Storage>.OnSuccess(new Storage(id, name, description));
    }

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
}


    
