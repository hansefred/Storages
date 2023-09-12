using System.Data;

namespace CleanArchitecture.Infrastructure.Repositories
{
    internal class BaseRepository
    {
        internal IDbTransaction Transaction { get; set; }
        internal IDbConnection Connection { 
            get
            {
                if (Transaction.Connection is null)
                    throw new NotImplementedException();
                return Transaction.Connection;
            }
        }

        public BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
