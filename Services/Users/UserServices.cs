using System;
using System.IO;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using repopractise.Domain.Repositories;
using System.Threading.Tasks;
using repopractise.Helpers;

namespace repopractise.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserServices(IUnitofwork unitofwork, IUserRepository userRepository, IMapper mapper,
        ILogger<UserServices> logger, IWebHostEnvironment webHostEnvironment) 
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ApiResponse<Domain.Dtos.User.UserUpdateBioResponse>> UpdateUserBio(Domain.Dtos.User.UserUpdateBioRequest user) 
        {

            Domain.Models.Users userRecord = await _userRepository.GetUserByEmail(user.Email);

            ApiResponse<Domain.Dtos.User.UserUpdateBioResponse> response = new ApiResponse<Domain.Dtos.User.UserUpdateBioResponse>();

            try
            {
                if (userRecord != null)
                {

                    userRecord.Firstname = user.Firstname;
                    userRecord.Lastname = user.Lastname;
                    userRecord.ProfileImage = handleProfilePictureUpload(user, userRecord.ProfileImage);

                    _userRepository.Update(userRecord);
                    await _unitofwork.CompleteAsync();

                    response.Data = _mapper.Map<Domain.Dtos.User.UserUpdateBioResponse>(userRecord);
                    response.Message = "user bio successfully updated";
                    response.Status = ApiResponseCodes.Success;

                    return response;

                }

                response.Message = "invalid email address";
                response.Data = null;
                response.Status = ApiResponseCodes.BadRequest;

                return response;
            }
            catch (Exception ex)
            {

                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Message = "An error occured while updating user bio";
                response.Data = null;
                response.Status = ApiResponseCodes.BadRequest;

                return response;
            }

        }

        private string handleProfilePictureUpload(Domain.Dtos.User.UserUpdateBioRequest user, string existingProfileImage)
        {
            string filename = existingProfileImage;
            if(user.ProfileImage != null) 
            {
                Console.WriteLine(_webHostEnvironment.ContentRootPath);
                string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
                filename = Guid.NewGuid().ToString() + "_" + user.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.ProfileImage.CopyTo(fileStream);
                }
            }

            return filename;
        }
    }
}