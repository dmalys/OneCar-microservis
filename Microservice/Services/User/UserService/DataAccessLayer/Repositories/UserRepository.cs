using LinqToDB;
using UserService.DataAccessLayer.Entities;
using UserService.DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace UserService.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, int>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<UserEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.UserId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<UserEntity>().DeleteAsync(x => x.UserId == id);
            }
        }

        public async Task<bool> CheckUserExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.UserId == identity))?.UserId > 0;
        }

        public async Task DeleteAsyncByAccountId(int accountId)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<UserEntity>().DeleteAsync(x => x.AccountId == accountId);
            }
        }
    }
}
