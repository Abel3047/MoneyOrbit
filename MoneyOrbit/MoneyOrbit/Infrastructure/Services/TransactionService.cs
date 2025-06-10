using MoneyOrbit.Application.DTOs.TransactionDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Infrastructure.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository<ITransaction> _transactionRepository;
        private readonly IAccountRepository<IAccount> _accountRepository;

        public TransactionService(ITransactionRepository<ITransaction> transactionRepository, 
            IAccountRepository<IAccount> accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ResultObject> RecordTransaction(TransactionRecordDto trDTO)
        {
            if(trDTO == null)
                return new ResultObject() { Error = "Transaction record data is null." };
            if (trDTO.Amount == 0)
                return new ResultObject() { Error = "Transaction record has to have an amount to be recorded." };
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

            return new ResultObject();
        }

        #region Support methods
        /// <summary>
        /// Checks if the account already exists in the database by simply running the typical path and if account!=null it will 
        /// return true
        /// </summary>
        /// <param name="username"></param>
        /// <returns> False if it does not exist in the database</returns>
        private async Task<bool> DoesAccountExist(string accID)
        {
            var account = await _accountRepository.GetInstanceOfType<Account>(accID);
            return account != null;
        }
        
        #endregion
    }
}

