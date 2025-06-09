using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IFactories
{
    internal interface IAccountFactory<TAccount> :IFactory<TAccount> where TAccount : IAccount
    {
        /// <summary>
        /// Creates an account with the <paramref name="_accountName"/>, and registers it under the user with the userID
        /// <paramref name="_userID"/>, generates a unique ID and passes in whether or not its an asset, liability, or 
        /// capital account. Also allows for an optional description.
        /// </summary>
        /// <param name="_accountName"></param>
        /// <param name="_isAsset"></param>
        /// <param name="_isLiability"></param>
        /// <param name="_isCaptial"></param>
        /// <param name="_description"></param>
        /// <returns></returns>
        Task<Account> CreateAccount(string _userID,string _accountName, bool _isAsset, bool _isLiability, bool _isCaptial, string? _description);
    }
}
