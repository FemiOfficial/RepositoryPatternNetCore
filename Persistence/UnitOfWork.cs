using repopractise.Domain.Repositories;
using repopractise.Persistence.Repositories;
using repopractise.Persistence.Context;
using System.Threading.Tasks;

namespace repopractise.Persistence
{
    public class UnitOfWork : IUnitofwork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context) 
        {
            _context = context;
            Users = new UserRepository(context);
            Accounts = new AccountRepository(context);
        }

        public IUserRepository Users { get; private set; }
        public IAccountRepository Accounts { get; private set; }

        public async Task<int> CompleteAsync() 
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}