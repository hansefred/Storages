using Microsoft.Data.SqlClient;
using System.Data;

namespace CleanArchitecture.Infastructure.Common
{
    public class DBConnectionFactory : IDBConnectionFactory
    {
        private readonly IDBConnectionModel _connectionModel;

        public DBConnectionFactory(IDBConnectionModel connectionModel)
        {
            _connectionModel = connectionModel;
        }

        public IDbConnection GetDBConnection()
        {
            var sql = new SqlConnection(_connectionModel.GetConnectionString());
            return sql;
        }
    }
}
