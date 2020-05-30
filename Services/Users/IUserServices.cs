
using System.Threading.Tasks;
using repopractise.Helpers;

namespace repopractise.Services.Users
{
    public interface IUserServices
    {
        Task<ApiResponse<Domain.Dtos.User.UserUpdateBioResponse>> UpdateUserBio(Domain.Dtos.User.UserUpdateBioRequest user);

    }
}