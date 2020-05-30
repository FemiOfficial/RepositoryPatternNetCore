using System.Threading.Tasks;
using repopractise.Domain.Repositories;
using repopractise.Domain.Models;
using repopractise.Domain.Dtos.Accounts;
using repopractise.Helpers;

namespace repopractise.Services.Accounts
{
    public interface IAccountService
    {
        Task<ApiResponse<CreateAccountResponseDto>> CreateAccount(CreateAccountRequestDto newaccount);
        Task<ApiResponse<UpdateAccountResponseDto>> UpdateAccount(UpdateAccountRequestDto accountUpdate);
        Task<ApiResponse<UpdateAccountResponseDto>> DeleteAccount(string accountNumber);


    }
}