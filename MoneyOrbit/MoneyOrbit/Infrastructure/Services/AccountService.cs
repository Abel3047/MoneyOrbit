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
            int trueCount = Convert.ToInt32(aCD.isAsset) + Convert.ToInt32(aCD.isLiability) + Convert.ToInt32(aCD.isCaptial);
            //If more than one of the account types are true, then it is invalid
            if (trueCount != 1) 
                return new ResultObject() { Error = "Exactly one of Asset, Liability, or Capital must be true." };

            // Check if the account under the username already exists
            try
            {                
                if (await DoesAccountExist(aCD.AccountName, aCD.userID))
                    return new ResultObject() { Error = "An account under this user with this name already exists" };
            }
            catch (Exception)
            {
                return new ResultObject() { Error = $"A user with this id {aCD.userID} is unable to be located" };
                throw;
            }
            
            //Creates the account with information
            var account = await new AccountFactory(_userRepository)
                .CreateAccount(aCD.userID ,aCD.AccountName,aCD.isAsset,aCD.isLiability, aCD.isCaptial,aCD.description);

            //Stores info in the database
            await _accountRepository.UpdateData(account.ID, account);
            return  new ResultObject() { Result= account.ID};
        }
        public async Task<ResultObject> UpdateAccount(AccountUpdateDto aUD)
        {
            var account = await _accountRepository.GetInstanceOfType<Account>(aUD.ID);
            if (!String.IsNullOrEmpty(aUD.description)) account.description = aUD.description;

            //Checks if the account type is valid
            int trueCount = Convert.ToInt32(aUD.isAsset) + Convert.ToInt32(aUD.isLiability) + Convert.ToInt32(aUD.isCaptial);
            //If more than one of the account types are true, then it is invalid
            if (trueCount != 1)
                return new ResultObject() { Error = "Exactly one of Asset, Liability, or Capital must be true." };

            //Sets the account type
            account.isAsset = aUD.isAsset; account.isLiability = aUD.isLiability; account.isCaptial = aUD.isCaptial;

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
            if (user == null) throw new Exception($"Unable to find user with {userID} in the database");
            return user.AccountIDs.Contains(accountName);
        }
        #endregion
    }
}
