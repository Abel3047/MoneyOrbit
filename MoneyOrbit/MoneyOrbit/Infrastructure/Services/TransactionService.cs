using MoneyOrbit.Application.DTOs.TransactionDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;
using System.Collections.ObjectModel;

namespace MoneyOrbit.Infrastructure.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository<ITransaction> _transactionRepository;
        private readonly IAccountRepository<IAccount> _accountRepository;
        private readonly IUserRepository<IUser> _userRepository;

        public TransactionService(ITransactionRepository<ITransaction> transactionRepository, 
            IAccountRepository<IAccount> accountRepository, IUserRepository<IUser> userRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultObject> GetUserTransactions(GetTransactionDto gTDTO)
        {
            //Checks if the userID is null or empty, which is a required parameter to get transactions
            if (String.IsNullOrEmpty(gTDTO.userID)) return new ResultObject() { Error = "User ID is required to get transactions." };

            //Gets the user from the userID
            var user= await _userRepository.GetInstanceOfType<User>(gTDTO.userID);
            //Checks if the user exists in the database
            if(NullGuard.IsNull(user)) return new ResultObject() { Error = $"User was not found with the ID {gTDTO.userID}." };

            //Gets the transactions from the acountIDs from the user
            List<Transaction> transactions = new List<Transaction>();
            foreach (var accID in user.AccountIDs)
            {
                var ts = await _transactionRepository.GetCollectionWithIdenticalProperty<Transaction>(accID);
                if (ts != null && ts.Any())
                {
                    transactions.AddRange(ts);
                }
            }

            #region Filtering options
            // Checks if the accountIDs to filter by is null or empty, which is an optional parameter
            if (gTDTO.AccIDs != null && gTDTO.AccIDs.Any())
            {
                transactions = transactions
                    .Where(t => gTDTO.AccIDs.Contains(t.AccDebitedID) || gTDTO.AccIDs.Contains(t.AccCreditedID)).ToList();
            }
            // Checks if the start date and end date are both set, and filters the transactions by date range
            if (gTDTO.StartDate.HasValue && gTDTO.EndDate.HasValue && gTDTO.StartDate <= gTDTO.EndDate)
            {
                transactions = transactions
                    .Where(t => t.Date >= gTDTO.StartDate.Value && t.Date <= gTDTO.EndDate.Value).ToList();
            }
            else if (gTDTO.StartDate.HasValue)
            {
                transactions = transactions
                    .Where(t => t.Date >= gTDTO.StartDate.Value).ToList();
            }
            else if (gTDTO.EndDate.HasValue)
            {
                transactions = transactions
                    .Where(t => t.Date <= gTDTO.EndDate.Value).ToList();
            }

            #endregion

            if (NullGuard.IsNull(transactions) || !transactions.Any())
                return new ResultObject() { Error = "No transactions were found for the user. Either adjust your filtering options " +
                    "or give a different user" };

            return new ResultObject()
            {
                Result = new Collection<Transaction>(transactions)
            };
        }
        public async Task<ResultObject> RecordTransaction(TransactionRecordDto trDTO)
        {
            if(trDTO == null)
                return new ResultObject() { Error = "Transaction record data is null." };
            if (trDTO.Amount == 0)
                return new ResultObject() { Error = "Transaction record has to have an amount to be recorded." };
            if(trDTO.Date == default(DateTime) || trDTO.Date == DateTime.MinValue|| NullGuard.IsNull(trDTO.Date))
                return new ResultObject() { Error = "Transaction record has to have a date to be recorded." };

            //If either of the transaction is null, it will set the value to a suspense account registered with the user
            //If both of them are null it will throw the typical error
            //Checks if the important information (accountDebited) is not null or empty
            if (String.IsNullOrEmpty(trDTO.AccDebitedID))
                return new ResultObject()
                {
                    Error = "You are missing an important piece of information. Please provide  the ID of the account" +
                    " debited-'AccDebitedID'."
                };
            //Checks if the account exists in the database
            if (!await DoesAccountExist(trDTO.AccDebitedID))
                return new ResultObject() { Error = "The debited account does not exist in the database, and is a requisite parameter" };

            //Creates a transaction object from the DTO
            var transaction = new TransactionFactory()
                .CreateTransaction(trDTO.Description,trDTO.AccDebitedID,trDTO.AccCreditedID, trDTO.Amount, trDTO.Date);
            //Records the transaction in the database
            await _transactionRepository.UpdateData(transaction.ID,transaction);

            return new ResultObject() { Result = "success" };
        }

        #region Support methods
        /// <summary>
        /// Checks if the account already exists in the database by simply running the typical path and if account!=null it will 
        /// return true
        /// </summary>
        /// <param name="accID"></param>
        /// <returns> False if it does not exist in the database</returns>
        private async Task<bool> DoesAccountExist(string accID)
        {
            var account = await _accountRepository.GetInstanceOfType<Account>(accID);
            return account != null;
        }
        
        #endregion
    }
}

