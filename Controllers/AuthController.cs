using Microsoft.AspNetCore.Mvc;
using repopractise.Domain.Dtos.User;
using repopractise.Services.Auth;
using repopractise.Helpers;
using System.Threading.Tasks;

namespace repopractise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        {
            _authService = authService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginDto loginuser) 
        {
            ApiResponse<UserAuthDto> response = await _authService.Login(loginuser);

            if(response.Status != ApiResponseCodes.Success) 
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserRegisterDto newuser)
        {
            ApiResponse<UserAuthDto> response = await _authService.Register(newuser, newuser.Password);

            if(response.Status != ApiResponseCodes.Created) 
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
    }
}