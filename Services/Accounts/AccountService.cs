using System;
using System.Threading.Tasks;
using AutoMapper;
using repopractise.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using repopractise.Domain.Dtos.Accounts;
using repopractise.Domain.Repositories;
using repopractise.Helpers;
using repopractise.Helpers.Utils;

namespace repopractise.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IUtils _utils;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _acccountRepository;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public AccountService(IUnitofwork unitofwork, IUserRepository userRepository, IAccountRepository accountRepository,IMapper mapper,
        ILogger<AccountService> logger, IConfiguration configuration, IUtils utils) 
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _userRepository = userRepository;
            _acccountRepository = accountRepository;
            _logger = logger;
            _utils = utils;
            _configuration = configuration;
        }

        public async Task<ApiResponse<CreateAccountResponseDto>> CreateAccount(CreateAccountRequestDto newaccount) 
        {
            ApiResponse<CreateAccountResponseDto> response = new ApiResponse<CreateAccountResponseDto>();

            try
            {
                Domain.Models.Accounts account = _mapper.Map<Domain.Models.Accounts>(newaccount);

                account.AccountNumber = _utils.GenerateAccountNumber(9);
                account.AccountBalance = newaccount.AccountOpeningBalance;
                account.createdAt = DateTime.Now;
                account.updatedAt = DateTime.Now;
                account.UserId = _userRepository.GetUserIdFromJWToken();
                account.User = await _userRepository.GetById(account.UserId);

                await _acccountRepository.AddAsync(account);

                await _unitofwork.CompleteAsync();


                response.Message = "Account successfully created";
                response.Data = _mapper.Map<CreateAccountResponseDto>(account);
                response.Status = ApiResponseCodes.Created;

                return response;

            }
            catch (Exception ex)
            {

                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Message = "An error occured while creating account";
                response.Data = null;
                response.Status = ApiResponseCodes.BadRequest;

                return response;

            }
        }

        public async Task<ApiResponse<UpdateAccountResponseDto>> UpdateAccount(UpdateAccountRequestDto accountUpdate) {
            
            ApiResponse<UpdateAccountResponseDto> response = new ApiResponse<UpdateAccountResponseDto>();

            try
            {
                Domain.Models.Accounts account = await _acccountRepository.GetByAccountNumber(accountUpdate.AccountNumber);

                if (account != null) 
                {
                    if (accountUpdate.Status != null) 
                    {
                        account.Status = accountUpdate.Status;
                    }

                    if (accountUpdate.AccountType != 0)
                    {
                        account.AccountType = accountUpdate.AccountType;
                    }

                    if (accountUpdate.AccountBalance != 0) 
                    {
                        account.AccountBalance = accountUpdate.AccountBalance;
                    }

                    _acccountRepository.Update(account);
                    await _unitofwork.CompleteAsync();

                    response.Status = ApiResponseCodes.Created;
                    response.Message = "Account was successfully updated";
                    response.Data = _mapper.Map<UpdateAccountResponseDto>(account);


                } 
                else 
                {

                    response.Status = ApiResponseCodes.BadRequest;
                    response.Message = "Invalid Account Number";
                    response.Data = null;

                }

                return response;
                
            }
            catch (Exception ex)
            {

                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Message = "An error occured while updating account";
                response.Data = null;
                response.Status = ApiResponseCodes.BadRequest;

                return response;
            }

        }

        public async Task<ApiResponse<UpdateAccountResponseDto>> DeleteAccount(string accountNumber) 
        {
            ApiResponse<UpdateAccountResponseDto> response = new ApiResponse<UpdateAccountResponseDto>();

            if (_userRepository.GetRoleFromJWToken() != "staff")
            {
                response.Message = "Restricted to only a staff";
                response.Status = ApiResponseCodes.BadRequest;
                response.Data = null;
                return response;
            }

            try
            {
                Domain.Models.Accounts account = await _acccountRepository.GetByAccountNumber(accountNumber);

                if (account != null) 
                {

                    _acccountRepository.Remove(account);
                    await _unitofwork.CompleteAsync();

                    response.Message = "Successfully Deleted Account";
                    response.Status = ApiResponseCodes.Success;
                    return response;
                }

                response.Message = "Invalid Account Number";
                response.Status = ApiResponseCodes.BadRequest;
                response.Data = null;
                return response;


            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Message = "An error occured while deleting account";
                response.Data = null;
                response.Status = ApiResponseCodes.BadRequest;

                return response;
            }
        }

        
    }
}