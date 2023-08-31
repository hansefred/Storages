namespace CleanArchitecture.Infastructure.Common
{
    internal class DBConnectionModel : IDBConnectionModel
    {
        public string Server { get; set; } = String.Empty;
        public string DBInstanze { get; set; } = String.Empty;

        public string Database { get; set; } = String.Empty;

        public string User { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;


        public string GetConnectionString()
        {
            if (String.IsNullOrEmpty(DBInstanze))
            {
                return $"Server={Server};Database={Database};User Id={User};Password={Password};";
            }

            return $"Server={Server}\\{DBInstanze};Database={Database};User Id={User};Password={Password};";

        }
    }
}
