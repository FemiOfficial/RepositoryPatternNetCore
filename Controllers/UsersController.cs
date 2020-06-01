using Microsoft.AspNetCore.Mvc;
using repopractise.Domain.Dtos.User;
using repopractise.Services.Users;
using repopractise.Services.Network;
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
        private readonly IWebService _webService;

        public UsersController(IUserServices userServices, IWebService webService) 
        {
            _userServices = userServices;
            _webService = webService;
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

        [HttpGet("GetOdata")]
        public async Task<IActionResult> GetOdata(string field) {
            var response = await _webService.GetOdataQueryData(field);

            if (response.Status != ApiResponseCodes.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}