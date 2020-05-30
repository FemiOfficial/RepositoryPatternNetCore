using repopractise.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using repopractise.Persistence.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repopractise.Domain.Models;
namespace repopractise.Persistence.Repositories
{
    public class AccountRepository : BaseRepository<Accounts>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context) {}

        public async Task<Accounts> GetAccountByUserId(int UserId) {
            Accounts account = await _context.Accounts.FirstOrDefaultAsync(x => x.UserId == UserId);
            return account;
        }
        public async Task<Accounts> GetById() {
            throw new NotImplementedException();
        }
        public async Task<Accounts> GetByAccountNumber(string accountNumber) 
        {
            Accounts account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            return account;
        }
    }
}