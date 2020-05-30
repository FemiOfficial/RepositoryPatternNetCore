using AutoMapper;
using repopractise.Domain.Models;
using repopractise.Domain.Dtos.Accounts;
using repopractise.Domain.Dtos.User;
using repopractise.Domain.Dtos.Transactions;


namespace repopractise
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<UserRegisterDto, Users>();
            CreateMap<CreateAccountRequestDto, Accounts>();
            CreateMap<Accounts, CreateAccountResponseDto>();
            CreateMap<Accounts, UpdateAccountResponseDto>();
            CreateMap<Users, UserUpdateBioResponse>();
            CreateMap<TransactionRequestDto, Transactions>();
            CreateMap<Transactions, TransactionResponseDto>();
        }
    }
}