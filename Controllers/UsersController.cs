using Microsoft.AspNetCore.Mvc;
using repopractise.Domain.Dtos.User;
using repopractise.Services.Users;
using repopractise.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace repopractise.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices) 
        {
            _userServices = userServices;
        }

        [HttpPatch("UpdateUserBio")]
        public async Task<IActionResult> UpdateUserBio([FromForm] UserUpdateBioRequest userBio) 
        {
            ApiResponse<UserUpdateBioResponse> response = await _userServices.UpdateUserBio(userBio);

            if (response.Status != ApiResponseCodes.Created)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}