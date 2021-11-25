using AccountService.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService.DataAccessLayer.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountEntity> GetAsync(int identity);
        Task<int> Insert(AccountEntity account);
        Task<int> Update(AccountEntity account);

        Task<List<AccountEntity>> GetAll();

        Task DeleteAsync(int id);
        Task<bool> CheckAccountExists(int identity);
    }
}
