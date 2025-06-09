using MoneyOrbit.Application.DTOs.AccountDtos;
using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<ResultObject> CreateAccount(AccountCreationDto accountCreationDto);
        Task<ResultObject> UpdateAccount(AccountUpdateDto uUD);
    }
}