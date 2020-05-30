using System.Threading.Tasks;
using repopractise.Domain.Repositories;
using repopractise.Domain.Models;
using repopractise.Domain.Dtos.User;
using repopractise.Helpers;

namespace repopractise.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse<UserAuthDto>> Register(UserRegisterDto user, string password);
        Task<ApiResponse<UserAuthDto>> Login(UserLoginDto signincredentials);
    }
}