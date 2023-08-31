using System.Data;

namespace CleanArchitecture.Infastructure.Common
{
    public interface IDBConnectionFactory
    {
        IDbConnection GetDBConnection();
    }
}