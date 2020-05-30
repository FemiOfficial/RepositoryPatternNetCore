using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using repopractise.Domain.Models;

namespace repopractise.Domain.Repositories
{
    public interface IAccountRepository : IBaseRepository<Accounts>
    {
        Task<Accounts> GetAccountByUserId(int UserId);
        Task<Accounts> GetById();
        Task<Accounts> GetByAccountNumber(string accountNumber);
    
        // Task<IEnumerable<Accounts>> GetTopperformingAccounts();

    }
}