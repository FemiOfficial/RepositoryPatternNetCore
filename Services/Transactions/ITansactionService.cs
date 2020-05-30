

using System.Threading.Tasks;
using repopractise.Helpers;
using repopractise.Domain.Dtos.Transactions;

namespace repopractise.Services.Transactions
{
    public interface ITansactionService
    {
        Task<ApiResponse<TransactionResponseDto>> DoTransaction(TransactionRequestDto newtransaction);


    }
}