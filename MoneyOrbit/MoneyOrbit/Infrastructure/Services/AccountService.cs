using MoneyOrbit.Application.Data.Repository;
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

        public AccountService(IAccountRepository<IAccount> accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public async Task<ResultObject> CreateAccount(AccountCreationDto aCD)
        {
            //Checks to see if the DTO is empty
            if (uCD == null)
                return new ResultObject() { Error = "User creation data is null." };

            //Checks if the important information is not null or empty
            if (String.IsNullOrEmpty(uCD.password) ||
               String.IsNullOrEmpty(uCD.UserName) ||
               String.IsNullOrEmpty(uCD.FirstName) ||
               String.IsNullOrEmpty(uCD.LastName) ||
               String.IsNullOrEmpty(uCD.AccessLevel))
                return new ResultObject() { Error = "You are missing an important piece of information.Please provide a" +
                    " 'ID'/password/firstname/lastname/AccessLevel."};

            //Checks if the email and phonenumber are in the correct format
            if (uCD.Email != null && !Validator.ValidateEmail(uCD.Email))
                return new ResultObject(){ Error = "This isn't the correct format for an email." };
            if (uCD.PhoneNumber != null && !Validator.ValidatePhoneNumber(uCD.PhoneNumber))
                return new ResultObject() { Error = "This isn't the correct format for a PhoneNumber." };

            // Check if the user already exists
            if (await DoesUserNameExist(uCD.UserName))
            return new ResultObject() { Error = "User already exists" };

            //Creates the user with information
            var user = new UserFactory()
                .CreateUser(uCD.UserName, uCD.FirstName, uCD.LastName, uCD.password, uCD.AccessLevel, uCD.Email, uCD.PhoneNumber);

            //Stores info in the database
            await _userRepository.UpdateData(user.ID, user);
            return  new ResultObject() { Result= user.ID};
        }
        public async Task UpdateAccount(AccountUpdateDto uUD)
        {
            User user = await _userRepository.GetInstanceOfType<User>(uUD.ID);
            if (!String.IsNullOrEmpty(uUD.FirstName)) user.FirstName = uUD.FirstName;
            if (!String.IsNullOrEmpty(uUD.LastName)) user.LastName = uUD.LastName;
            if (!String.IsNullOrEmpty(uUD.Email)) user.Email = uUD.Email;
            if (!String.IsNullOrEmpty(uUD.PhoneNumber)) user.PhoneNumber = uUD.PhoneNumber;
            await _userRepository.UpdateData(user.ID, user);
        }
        public async Task<Account> GetAccountById(string accountId) => await _accountRepository.GetInstanceOfType<Account>(accountId);        
        public async Task DeleteUser(string accountId) => await _accountRepository.DeleteData(accountId);

        #region Support methods
        /// <summary>
        /// Checks if the account already exists in the database by checking if the accountID is within a certain path configuration
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> False if it does not exist in the database</returns>
        private async Task<bool> DoesAccountExist(string ID)=> await _accountRepository.DoesPropertyExist(ID);
        #endregion
    }
}
