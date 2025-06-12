using MoneyOrbit.Application.DTOs.TransactionDtos;
using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface ITransactionService
    {
        /// <summary>
        /// This method takes the user Token gets all the accounts involved with the user. 
        /// <para>It then goes ahead to search all transactions involved with those accounts. Since those accounts are only involved
        /// with that user, there is no way more transactions could be involved.</para>
        /// <para>These transactions are then filtered according to the variables set in <paramref name="getTransactionDto"/>
        /// like date, whether you are looking for backlogs, or specific accounts</para>
        /// </summary>
        /// <param name="getTransactionDto"></param>
        /// <returns></returns>
        Task<ResultObject> GetUserTransactions(GetTransactionDto getTransactionDto);
        /// <summary>
        /// This method makes sure the required provided variables in <paramref name="transactionRecordDto"/> are valid. After which
        ///  it will check if the account credited is null or empty, and sets it to the suspense account if it is.
        ///  Once that is set it will create a transaction and save it to the database.
        /// </summary>
        /// <param name="transactionRecordDto"></param>
        /// <returns></returns>
        Task<ResultObject> RecordTransaction(TransactionRecordDto transactionRecordDto);
    }
}