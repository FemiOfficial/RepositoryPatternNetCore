using Microsoft.AspNetCore.Http;

namespace repopractise.Domain.Dtos.User
{
    public class UserUpdateBioRequest
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public IFormFile ProfileImage { get; set; }

    }
}