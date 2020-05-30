using System;
using System.Threading.Tasks;


namespace repopractise.Domain.Repositories
{
    public interface IUnitofwork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}