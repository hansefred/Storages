using System.Data;

namespace CleanArchitecture.Infastructure.Repositories
{
    internal class BaseRepository
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { 
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
