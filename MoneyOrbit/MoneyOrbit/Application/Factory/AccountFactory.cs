using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IApplication.IFactories;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Factory
{
    public class AccountFactory :BaseFactory<Account>, IAccountFactory<Account>
    {
        private readonly IUserRepository<IUser> _userRepository;

        public AccountFactory(IUserRepository<IUser> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Account> CreateAccount(string token, string _accountName, bool _isAsset, bool _isExpense, bool _isLiability, bool _isCaptial, string? _description)
        {
            //Generates ID to store in the new account and in the list of Users accounts
            var accountID = generators.GenerateKey(DateTime.Now);

            //Creates account with information
            Account _account = new Account()
            {
                AccountName=_accountName,
                description = _description,
                isAsset = _isAsset,
                isExpense= _isExpense,
                isLiability = _isLiability,
                isCaptial = _isCaptial,
                ID = accountID
            };

            //Gets the IDs in the Users list of accounts
            var user = await _userRepository.GetInstanceOfType<User>(token);
            //If there isn't, we need to make a new array and add our first account
            if (user.AccountIDs == null || user.AccountIDs.Length == 0) user.AccountIDs = new string[] { accountID };
            //else there is accounts already,simply append
            else user.AccountIDs= user.AccountIDs.Append(accountID).ToArray();
            //Stores the new ID in the User's list of accounts
            await _userRepository.UpdateData(user.ID, user);

            return _account;
        }
        public async Task<BankAccount> CreateBankAccount(string token, string accountName, string? description, string bankAccountName, string bankAccountNumber, string bankBranchName, string bankBranchCode,string? bankSwiftCode)
        {
            //Generates ID to store in the new account and in the list of Users accounts
            var accountID = generators.GenerateKey(DateTime.Now);

            //Creates account with information
            BankAccount _bankaccount = new BankAccount()
            {
                AccountName = accountName,
                description = description,
                ID = accountID,
                BankAccountName = bankAccountName,
                BankAccountNumber = bankAccountNumber,
                BankBranchName = bankBranchName,
                BankBranchCode = bankBranchCode,
                BankSwiftCode = bankSwiftCode
            };

            //Gets the IDs in the Users list of accounts
            var user = await _userRepository.GetInstanceOfType<User>(token);
            //If there isn't, we need to make a new array and add our first account
            if (user.AccountIDs == null || user.AccountIDs.Length == 0) user.AccountIDs = new string[] { accountID };
            //else there is accounts already,simply append
            else user.AccountIDs = user.AccountIDs.Append(accountID).ToArray();
            //Stores the new ID in the User's list of accounts
            await _userRepository.UpdateData(user.ID, user);

            return _bankaccount;
        }
    }
}
