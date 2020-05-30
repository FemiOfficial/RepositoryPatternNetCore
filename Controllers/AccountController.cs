using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using repopractise.Helpers;
using repopractise.Services.Accounts;
using repopractise.Domain.Dtos.Accounts;
using System.Threading.Tasks;

namespace repopractise.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(CreateAccountRequestDto newAccount) 
        {
            ApiResponse<CreateAccountResponseDto> response = await _accountService.CreateAccount(newAccount);

            if(response.Status != ApiResponseCodes.Created) {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
        [HttpPatch("UpdateAccount")]
        [Authorize(Policy = "StafforAdminOperations")]
        public async Task<IActionResult> UpdateAccount(UpdateAccountRequestDto updateAccount) 
        {
            ApiResponse<UpdateAccountResponseDto> response = await _accountService.UpdateAccount(updateAccount);

            if (response.Status == ApiResponseCodes.BadRequest) {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
    }
}