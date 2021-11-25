using AccountService.DataAccessLayer.Entities;
using LinqToDB;

namespace AccountService.DataAccessLayer
{
    public class OneCarDbConnection : LinqToDB.Data.DataConnection
    {
        public OneCarDbConnection(string connectionString) : base("SqlServer", connectionString) { }

        public ITable<AccountEntity> Account => GetTable<AccountEntity>();
    }
}
