using CleanArchitecture.Domain.Primitives;
using OneOf;


namespace CleanArchitecture.Domain;


public class Storage : AggregateRoot
{

    private Storage(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    private List<StorageArticle> _articles;

    public List<StorageArticle> StorageArticles => _articles;
    public string Name { get; private set;}
    public string Description { get; private set;}

    public static OneOf<Storage,StorageNameIsOutofRangeDomainException, StorageDescriptionIsOutofRangeDomainException> Create (Guid id, string name, string description)
    {
       if (name is null || name.Length < 5 || name.Length > 49)
       {
            return new StorageNameIsOutofRangeDomainException ("Storage Name must be betwenn 5 and 49 Chars");
       }
       if (description is null || description.Length > 149)
       {
            return new StorageDescriptionIsOutofRangeDomainException ("Storage Description can't be longer than 149 Chars");
       }

       return new Storage (id, name, description);
    }
}


    
