namespace CleanArchitecture.Infastructure.Common
{
    public interface IDBConnectionModel
    {
        string GetConnectionString();
        string GetPassword();
        string GetDBName();
    }
}