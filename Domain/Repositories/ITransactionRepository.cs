using System.Threading.Tasks;
using repopractise.Domain.Models;

namespace repopractise.Domain.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transactions>
    {
        Task<Transactions> GetTransactions();
        Task<Transactions> GetTransactionsByTransactionRef(string transactionRef);
        Task<Transactions> GetTransactionsByAccountNumber(string accountNumber);
    }
}