using MoneyOrbit.Application.DTOs.AccountDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Infrastructure.Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepository<IAccount> _accountRepository;
        private readonly IUserRepository<IUser> _userRepository;

        public AccountService(IAccountRepository<IAccount> accountRepository, IUserRepository<IUser> userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultObject> CreateAccount(AccountCreationDto aCD)
        {
            //Checks to see if the DTO is empty
            if (aCD == null)
                return new ResultObject() { Error = "Account creation data is null." };

            //Checks if the important information is not null or empty
            if (String.IsNullOrEmpty(aCD.AccountName) ||
               String.IsNullOrEmpty(aCD.userID))
                return new ResultObject() { Error = "You are missing an important piece of information.Please provide an" +
                    " 'AccountName'/userID"};

            //Checks if the account type is valid
            int trueCount = Convert.ToInt32(aCD.isAsset) + Convert.ToInt32(aCD.isLiability)
                            + Convert.ToInt32(aCD.isCaptial)+ Convert.ToInt32(aCD.isExpense);
            //If more than one of the account types are true, then it is invalid
            if (trueCount != 1) 
                return new ResultObject() { Error = "Exactly one of Asset, Liability, or Capital must be true." };

            // Check if the account under the username already exists
            try
            {                
                if (await DoesAccountExist(aCD.AccountName, aCD.userID))
                    return new ResultObject() { Error = "An account under this user with this name already exists" };
                if (aCD.AccountName.Contains("Suspense", StringComparison.OrdinalIgnoreCase))
                    return new ResultObject() { Error = "The Suspense account already exists" };
            }
            catch (ArgumentNullException)
            {
                return new ResultObject() { Error = $"A user with this id {aCD.userID} is unable to be located" };
                throw;
            }
            
            //Creates the account with information
            var account = await new AccountFactory(_userRepository)
                .CreateAccount(aCD.userID ,aCD.AccountName,aCD.isAsset, aCD.isExpense,aCD.isLiability, aCD.isCaptial,aCD.description);

            //Stores info in the database
            await _accountRepository.UpdateData(account.ID, account);
            return  new ResultObject() { Result= account.ID};
        }
        public async Task<ResultObject> UpdateAccount(AccountUpdateDto aUD)
        {
            var account = await _accountRepository.GetInstanceOfType<Account>(aUD.ID);
            if (!String.IsNullOrEmpty(aUD.description)) account.description = aUD.description;

            //Checks if the account type is valid
            int trueCount = Convert.ToInt32(aUD.isAsset) + Convert.ToInt32(aUD.isLiability) 
                            + Convert.ToInt32(aUD.isCaptial)+ Convert.ToInt32(aUD.isExpense);
            //If more than one of the account types are true, then it is invalid
            if (trueCount != 1)
                return new ResultObject() { Error = "Exactly one of Asset, Liability, or Capital must be true." };

            //Sets the account type
            account.isAsset = aUD.isAsset; account.isExpense = aUD.isExpense;
            account.isLiability = aUD.isLiability; account.isCaptial = aUD.isCaptial; 

            await _accountRepository.UpdateData(account.ID, account);
            return new ResultObject() { Result = "success" };
        }
        public async Task<Account> GetAccountById(string accountId) => await _accountRepository.GetInstanceOfType<Account>(accountId);        
        public async Task<ResultObject> DeleteUser(string accountId)
        {
            await _accountRepository.DeleteData(accountId);
            return new ResultObject() { Result = "success" };
        }

        #region Support methods
        /// <summary>
        /// Checks if the account already exists under the user with the id, <paramref name="userID"/>
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> False if it does not exist in the database</returns>
        private async Task<bool> DoesAccountExist(string accountName, string userID)
        {
            var user= await _userRepository.GetInstanceOfType<User>(userID);
            if (NullGuard.IsNull(user)) throw new Exception($"Unable to find user with {userID} in the database");
            if(user.AccountIDs==null || user.AccountIDs.Length==0) return false;

            //A better to get multiple accounts at once and sort through them, than make multiple requests from different consumers of the 
            //API   
            //Takes all the accounts that have the same name as the <paramref name="accountName"/> and returns them
            var similarAccounts = await _accountRepository.GetCollectionWithIdenticalProperty<Account>(accountName);
            //Checks if any of them have the same ID as the ones registered with the user
            foreach (var account in similarAccounts)
            {
                if (NullGuard.IsNull(account)) continue; // If the account is null, skip to the next iteration
                if (user.AccountIDs.Contains(account.ID, StringComparer.OrdinalIgnoreCase))
                    return true; // Account with the same name exists under this user
            }
            return false;
        }
        #endregion
    }
}
