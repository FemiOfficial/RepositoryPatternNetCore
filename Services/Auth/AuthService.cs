using repopractise.Persistence;
using repopractise.Domain.Repositories;
using repopractise.Domain.Dtos.User;
using repopractise.Helpers;
using System;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Cryptography;
using repopractise.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace repopractise.Services.Auth
{

    public class AuthService : IAuthService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public AuthService(IUnitofwork unitofwork, IUserRepository userRepository, IMapper mapper,
        ILogger<AuthService> logger, IConfiguration configuration)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
            _configuration = configuration;

        }

        public async Task<ApiResponse<UserAuthDto>> Login(UserLoginDto signincredentials)
        {

            ApiResponse<UserAuthDto> response = new ApiResponse<UserAuthDto>();

            try
            {
                Domain.Models.Users user = await _userRepository.GetUserByEmail(signincredentials.Email);

                if (user == null)
                {
                    response.Status = ApiResponseCodes.NotFound;
                    response.Message = "Invalid Email Address";
                    response.Data = null;
                }
                else if (VerifyPasswordHash(signincredentials.Password, user.PasswordHash, user.PasswordSalt) == false)
                {
                    response.Status = ApiResponseCodes.BadRequest;
                    response.Message = "Invalid Password";
                    response.Data = null;
                }
                else
                {
                    response.Status = ApiResponseCodes.Success;
                    response.Message = "user successfully logged in";
                    response.Data = new UserAuthDto
                    {
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Token = GenerateToken(user)
                    };
                }

                return response;

            }
            catch (Exception ex)
            {

                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Status = ApiResponseCodes.ServerError;
                response.Message = "An error occured while logging user in, confirm you payload";
                response.Data = null;
                return response;
            }


        }

        public async Task<ApiResponse<UserAuthDto>> Register(UserRegisterDto newuser, string password)
        {
            ApiResponse<UserAuthDto> response = new ApiResponse<UserAuthDto>();

            try
            {

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                Domain.Models.Users user = _mapper.Map<Domain.Models.Users>(newuser);

                if (await UserExists(user.Email)) 
                {
                    response.Message = "Email Address already taken";
                    response.Data = null;
                    response.Status = ApiResponseCodes.Conflict;
                    return response;
                }
                
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.createdAt = DateTime.Now;
                user.updatedAt = DateTime.Now;

                await _userRepository.AddAsync(user);
                await _unitofwork.CompleteAsync();

                response.Status = ApiResponseCodes.Created;
                response.Message = "Account created successfully";
                response.Data = new UserAuthDto {
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Token = GenerateToken(user)
                };

                return response;

            }
            catch (Exception ex)
            {

                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Status = ApiResponseCodes.BadRequest;
                response.Message = "An error occured while creating user, confirm you payload";
                response.Data = null;
                return response;
            }

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private async Task<bool> UserExists(string email)
        {
            if(await _userRepository.GetUserByEmail(email) == null) {
                return false;
            }

            return true;
        }

        private string GenerateToken(Domain.Models.Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Firstname + " " + user.Lastname),
                new Claim("Role", user.Type),
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}