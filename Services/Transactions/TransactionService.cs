using System;
using System.Threading.Tasks;
using AutoMapper;
using repopractise.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using repopractise.Domain.Repositories;
using repopractise.Helpers;
using repopractise.Helpers.Utils;
using repopractise.Domain.Dtos.Transactions;
using repopractise.Domain.Models;

namespace repopractise.Services.Transactions
{
    public class TransactionService : ITansactionService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IUtils _utils;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRespository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger _logger;

        public TransactionService(IUnitofwork unitofwork, ITransactionRepository transactionRepository, IMapper mapper,
        ILogger<TransactionService> logger, IUtils utils, IUserRepository userRepository, IAccountRepository accountRepository) 
        {

            _unitofwork = unitofwork;
            _logger = logger;
            _mapper = mapper;
            _utils = utils;
            _transactionRepository = transactionRepository;
            _userRespository = userRepository;
            _accountRepository = accountRepository;

        }

        public async Task<ApiResponse<TransactionResponseDto>> DoTransaction(TransactionRequestDto newtransaction) 
        {
            ApiResponse<TransactionResponseDto> response = new ApiResponse<TransactionResponseDto>();

            try 
            {

                // check current available balance if transaction is debit

                if (newtransaction.TransactionType.ToLower() == "debit") 
                {

                    float sendCurrentAvailableBal = await GetAvailableBalanceForSender(newtransaction.Sender);

                    Boolean isValid = isDebitLowerThanAvailableBalance(newtransaction.Amount, sendCurrentAvailableBal);

                    if(!isValid) 
                    {

                        response.Message = "Available balance to debit from sender is lower than specied amount";
                        response.Data = null;
                        response.Status = ApiResponseCodes.Success;
                        
                        return response;

                    }
                    
                }

                Domain.Models.Transactions transaction = _mapper.Map<Domain.Models.Transactions>(newtransaction);

                transaction.Cashier = _userRespository.GetUserIdFromJWToken();
                transaction.TransactionType = newtransaction.TransactionType.ToLower() == "debit"
                    ? ETransactionType.Debit : ETransactionType.Credit;

                transaction.createdAt = DateTime.Now;
                transaction.updatedAt = DateTime.Now;

                transaction.TransactionReference = await GenerateTransactionReference();
                transaction.RecipientAccountNumber = newtransaction.Recipient;
                transaction.SenderAccountNumber = newtransaction.Sender;

                Domain.Dtos.Transactions.TransactionResponseDto responseData = _mapper.Map<Domain.Dtos.Transactions.TransactionResponseDto>(transaction);
                responseData.TransanctionTime = transaction.createdAt;
                responseData.TransactionType = transaction.TransactionType == ETransactionType.Debit ? "Debit" : "Credit";

                response.Data = responseData;
                response.Status = ApiResponseCodes.Success;
                response.Message = "Transaction Successful";

                return response;

            } 
            catch(Exception ex) 
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Message = "An error occured while creating account";
                response.Data = null;
                response.Status = ApiResponseCodes.BadRequest;

                return response;
            }
            
        }
       

        private Boolean isDebitLowerThanAvailableBalance(float debitAmount, float availableBalance) 
        {
            if(debitAmount > availableBalance) return true;

            return false;
        }

        private async Task<float> GetAvailableBalanceForSender(string sendAccountNumber) {
            Domain.Models.Accounts senderAccountDetails = await _accountRepository.GetByAccountNumber(sendAccountNumber);
            return senderAccountDetails.AccountBalance;
        }

        private async Task<string> GenerateTransactionReference() {
            string transactionRef = "BANKA-" + _utils.GenerateTransactionReference();
            var conflictRef = await _transactionRepository.GetTransactionsByTransactionRef(transactionRef);

            while (conflictRef != null)
            {
                transactionRef = "BANKA-" + _utils.GenerateTransactionReference();
                conflictRef = await _transactionRepository.GetTransactionsByTransactionRef(transactionRef);
            }

            return transactionRef;
        }




    }
}