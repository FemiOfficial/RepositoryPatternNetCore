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
    public class TransactionRespository : BaseRepository<Transactions>, ITransactionRepository
    {
        public TransactionRespository(AppDbContext context) : base(context) {}

        public async Task<Transactions> GetTransactionsByTransactionRef(string transactioiionRef) 
        {
            Transactions transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionReference == transactioiionRef);
            return transaction;
        }

        public async Task<Transactions> GetTransactions()
        {
            throw new NotImplementedException();
        }

        public async Task<Transactions> GetTransactionsByAccountNumber(string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}