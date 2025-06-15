using MoneyOrbit.Application.DTOs.AccountDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;
using System.Net.Http.Headers;

namespace MoneyOrbit.Infrastructure.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository<IAccount> _accountRepository;
        private readonly IUserRepository<IUser> _userRepository;
        private readonly IHttpClientService _httpclientService;

        public AccountService(IAccountRepository<IAccount> accountRepository, IUserRepository<IUser> userRepository,
            IHttpClientService httpclientService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _httpclientService = httpclientService;
        }

        public async Task<ResultObject> CreateAccount(CreateAccountDto aCD)
        {
            //Checks to see if the DTO is empty
            if (NullGuard.IsNull(aCD))
                return new ResultObject() { Error = "Account creation data is null." };

            //Checks if the important information is not null or empty
            if (String.IsNullOrEmpty(aCD.AccountName) ||
               String.IsNullOrEmpty(aCD.Token))
                return new ResultObject() { Error = "You are missing an important piece of information.Please provide an" +
                    " 'AccountName'/Token"};

            //Checks if the account type is valid
            int trueCount = Convert.ToInt32(aCD.isAsset) + Convert.ToInt32(aCD.isLiability)
                            + Convert.ToInt32(aCD.isCaptial)+ Convert.ToInt32(aCD.isExpense);
            //If more than one of the account types are true, then it is invalid
            if (trueCount != 1) 
                return new ResultObject() { Error = "Exactly one of Asset, Liability, or Capital must be true." };

            // Check if the account under the username already exists
            try
            {                
                if (await DoesAccountExist(aCD.AccountName, aCD.Token))
                    return new ResultObject() { Error = "An account under this user with this name already exists" };
                if (aCD.AccountName.Contains("Suspense", StringComparison.OrdinalIgnoreCase))
                    return new ResultObject() { Error = "The Suspense account already exists" };
            }
            catch (ArgumentNullException)
            {
                return new ResultObject() { Error = $"A user with this id {aCD.Token} is unable to be located" };
                throw;
            }
            
            //Creates the account with information
            var account = await new AccountFactory(_userRepository)
                .CreateAccount(aCD.Token ,aCD.AccountName,aCD.isAsset, aCD.isExpense,aCD.isLiability, aCD.isCaptial,aCD.description);

            //Stores info in the database
            await _accountRepository.UpdateData(account.ID, account);
            return  new ResultObject() { Result= account.ID};
        }
        public async Task<ResultObject> UpdateAccount(UpdateAccountDto aUD)
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
        public async Task<ResultObject> DeleteAccount(DeleteAccountDto dADTO)
        {
            await _accountRepository.DeleteData(dADTO.accountId);
            var emptyaccount = await _accountRepository.GetInstanceOfType<Account>(dADTO.accountId);
            if(NullGuard.IsNotNull(emptyaccount)) return new ResultObject() { Error = $"Failed to delete the account with the ID {dADTO.accountId}" };

            return new ResultObject() { Result = "success" };
        }
        public async Task<ResultObject> RegisterWithAccountNumber(RegisterWithAccountNumberDto rWANDto)
        {
            var request=await RequestLinkageWithBank(rWANDto);
            if(NullGuard.IsNotNull(request.Error)) return new ResultObject() { Error = $"Failed to send request to the bank. Because of {request.Error} Please try again later." };
            //INotification system should be used to notify the user that the request has been sent to the bank
            return await CreateBankAccount(rWANDto);
        }
        public async Task<ResultObject> RegisterWithSecurityCode(RegisterWithSecurityCodeDto registerWithSecurityCodeDto)
        {
            var request = await RequestLinkageWithBank(registerWithSecurityCodeDto);
            if (NullGuard.IsNotNull(request.Error)) return new ResultObject() { Error = $"Failed to send request to the bank. Because of {request.Error} Please try again later." };
            //INotification system should be used to notify the user that the request has been sent to the bank
            return await CreateBankAccount(registerWithSecurityCodeDto);
        }

        #region Support methods
        /// <summary>
        /// Checks if the account already exists under the user with the id, <paramref name="token"/>
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> False if it does not exist in the database</returns>
        private async Task<bool> DoesAccountExist(string accountName, string token)
        {
            var user= await _userRepository.GetInstanceOfType<User>(token);
            if (NullGuard.IsNull(user)) throw new Exception($"Unable to find user with {token} in the database");
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
        private async Task<ResultObject> RequestLinkageWithBank(object rWANDto)
        {
            var bankapiUrl = Environment.GetEnvironmentVariable("BankAPI_Basepath");
            using (var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, bankapiUrl))
            {
                requestMessage.Headers.Add("User-Agent", "MoneyOrbit");
                requestMessage.Headers.Add("Authorization", $"token {Environment.GetEnvironmentVariable("BankAPI_accessToken")}");

                var content = JsonContent.Create(rWANDto,
                    new MediaTypeHeaderValue("application/json"));
                //Here we set the content of the request message with the object we just created
                requestMessage.Content = content;

                try
                {
                    var response = await _httpclientService.Request(requestMessage);
                    return new ResultObject() { Result = response };
                }
                catch (Exception ex)
                {
                    return new ResultObject() { Error = ex.Message };
                }
            }
        }
        private async Task<ResultObject> CreateBankAccount(BaseRegisterBankAccountDto aCD)
        {
            if (String.IsNullOrEmpty(aCD.BankAccountName) || String.IsNullOrEmpty(aCD.BankAccountNumber) ||
                               String.IsNullOrEmpty(aCD.BankBranchName) || String.IsNullOrEmpty(aCD.BankBranchCode))
                return new ResultObject()
                {
                    Error = "You cannot create a Bank Account without an AccountName,AccountNumber," +
                    "BranchName or BankBranch code"
                };
            //Creates a bank account with information
            var bankaccount = await new AccountFactory(_userRepository)
            .CreateBankAccount(aCD.Token, aCD.AccountName, aCD.description, aCD.BankAccountName, aCD.BankAccountNumber,
            aCD.BankBranchName, aCD.BankBranchCode, aCD.BankSwiftCode);

            //Stores info in the database
            await _accountRepository.UpdateData(bankaccount.ID, bankaccount);
            return new ResultObject() { Result = bankaccount.ID };
        }
        #endregion
    }
}
