using AccountService.BusinessLayer.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.BusinessLayer.Login.Interfaces
{
    public interface IAccountHandler
    {
        Task<GetAccountResponse> GetAccount(GetAccountRequest request);

        Task DeleteAccount(DeleteAccountRequest request);

        Task AddAccount(AddAccountRequest request);

        Task UpdateAccount(UpdateAccountRequest request);

        Task<GetAccountsResponse> GetAccounts(); //Only for admins
    }
}
