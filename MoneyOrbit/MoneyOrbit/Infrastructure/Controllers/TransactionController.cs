using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.DTOs.AccountDtos;
using MoneyOrbit.Application.DTOs.TransactionDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Infrastructure.Services;

namespace MoneyOrbit.Infrastructure.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpPost("RecordTransaction")]
        public async Task<ActionResult> RecordTransaction(TransactionRecordDto transactionRecordDto)
        {
            ResultObject resultObject = await _transactionService.RecordTransaction(transactionRecordDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok("Transaction recorded successfully.");
        }
        [HttpPost("DeleteTransaction")]
        public async Task<ActionResult> DeleteTransaction(DeleteTransactionDto deleteTransactionDto)
        {
            ResultObject resultObject = await _transactionService.DeleteTransaction(deleteTransactionDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok("Transaction deleted successfully.");
        }
        [HttpGet("GetUserTransactions")]
        public async Task<ActionResult<ResultObject>> GetUserTransactions(GetTransactionDto getTransactionDto)
        {
            ResultObject resultObject = await _transactionService.GetUserTransactions(getTransactionDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return resultObject;
        }
    }
}
