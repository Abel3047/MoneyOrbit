using MoneyOrbit.Application.DTOs.AccountDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<ResultObject> CreateAccount(AccountCreationDto accountCreationDto);
        Task<ResultObject> UpdateAccount(AccountUpdateDto aUD);
        Task<Account> GetAccountById(string accountId);
        Task<ResultObject> DeleteUser(string accountId);
    }
}