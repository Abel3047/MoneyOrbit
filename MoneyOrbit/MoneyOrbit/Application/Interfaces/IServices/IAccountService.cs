using MoneyOrbit.Application.DTOs.AccountDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IAccountService
    {
        /// <summary>
        /// This is used to create all accounts (including <see cref="BankAccount"/>s).
        /// <para>It makes sure that the dto has the necessary information, then makes sure that the account</para>
        /// </summary>
        /// <param name="accountCreationDto"></param>
        /// <returns></returns>
        Task<ResultObject> CreateAccount(CreateAccountDto accountCreationDto);
        Task<ResultObject> UpdateAccount(UpdateAccountDto aUD);
        Task<Account> GetAccountById(string accountId);
        Task<ResultObject> DeleteAccount(DeleteAccountDto deleteAccountDto);
        Task<ResultObject> RegisterWithAccountNumber(RegisterWithAccountNumberDto registerWithAccountNumberDto);
        Task<ResultObject> RegisterWithSecurityCode(RegisterWithSecurityCodeDto registerWithSecurityCodeDto);
    }
}