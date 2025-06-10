using MoneyOrbit.Application.DTOs.TransactionDtos;
using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface ITransactionService
    {
        Task<ResultObject> RecordTransaction(TransactionRecordDto transactionRecordDto);
    }
}