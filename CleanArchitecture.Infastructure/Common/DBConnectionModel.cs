namespace CleanArchitecture.Infastructure.Common
{
    public class DBConnectionModel : IDBConnectionModel
    {
        public DBConnectionModel(string server, string dBInstanze, string database, string user, string password)
        {
            Server = server;
            DBInstanze = dBInstanze;
            Database = database;
            User = user;
            Password = password;
        }
        public DBConnectionModel(string server,  string database, string user, string password): this(server,"",database,user,password)
        {
        }

        public string Server { get; set; } = String.Empty;
        public string DBInstanze { get; set; } = String.Empty;

        public string Database { get; set; } = String.Empty;

        public string User { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;


        public string GetConnectionString()
        {
            if (String.IsNullOrEmpty(DBInstanze))
            {
                return $"Server={Server};Database={Database};User Id={User};Password={Password};TrustServerCertificate=true;";
            }

            return $"Server={Server}\\{DBInstanze};Database={Database};User Id={User};Password={Password};TrustServerCertificate=true;";

        }

        public string GetDBName()
        {
            return Database;
        }

        public string GetPassword()
        {
            return Password;
        }
    }
}
