using LinqToDB;
using AccountService.DataAccessLayer.Entities;
using AccountService.DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AccountService.DataAccessLayer.Repository;

namespace AccountService.DataAccessLayer.Repositories
{
    public class AccountRepository : BaseRepository<AccountEntity, int>, IAccountRepository
    {
        public AccountRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<AccountEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.AccountId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<AccountEntity>().DeleteAsync(x => x.AccountId == id);
            }
        }

        public async Task<bool> CheckAccountExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.AccountId == identity))?.AccountId > 0;
        }
    }
}
