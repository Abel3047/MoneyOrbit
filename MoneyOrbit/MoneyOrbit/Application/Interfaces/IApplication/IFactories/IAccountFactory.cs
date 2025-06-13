using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IFactories
{
    internal interface IAccountFactory<TAccount> :IFactory<TAccount> where TAccount : IAccount
    {
        /// <summary>
        /// Creates an account with the <paramref name="_accountName"/>, and registers it under the user with the Token
        /// <paramref name="Token"/>, generates a unique Token and passes in whether or not its an asset, liability, or 
        /// capital account. Also allows for an optional description.
        /// </summary>
        /// <param name="_accountName"></param>
        /// <param name="_isAsset"></param>
        /// <param name="_isLiability"></param>
        /// <param name="_isCaptial"></param>
        /// <param name="_description"></param>
        /// <returns></returns>
        Task<Account> CreateAccount(string Token, string _accountName, bool _isAsset, bool _isExpense, bool _isLiability, bool _isCaptial, string? _description);
        /// <summary>
        /// This take in the requied informaiton for creating a BankAccount in the database. Note that this does not link the actual bank 
        /// account to the user. A separate endpoint is used for that.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accountName"></param>
        /// <param name="description"></param>
        /// <param name="bankAccountName"></param>
        /// <param name="bankAccountNumber"></param>
        /// <param name="bankBranchName"></param>
        /// <param name="bankBranchCode"></param>
        /// <returns></returns>
        Task<BankAccount> CreateBankAccount(string token, string accountName, string? description, string bankAccountName, string bankAccountNumber, string bankBranchName, string bankBranchCode, string? bankSwiftCode);
    }
}
