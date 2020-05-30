using System.Collections.Generic;
using System.Threading.Tasks;
using repopractise.Domain.Models;

namespace repopractise.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        Task<Users> GetById(int id);
        int GetUserIdFromJWToken();
        string GetRoleFromJWToken();
        string GetFullNameFromJWToken();
        string GetEmailFromJWToken();
        Task<Users> GetUserByEmail(string email);


    }
}