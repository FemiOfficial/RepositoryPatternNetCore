using repopractise.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using repopractise.Persistence.Context;
using System.Threading.Tasks;
using repopractise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace repopractise.Persistence.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository 
    {
        private readonly IHttpContextAccessor _httContextAccessor;

        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context) {
            _httContextAccessor = httpContextAccessor;
        }

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Users> GetById(int id) 
        {
            Users user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public int GetUserIdFromJWToken() {
            return int.Parse(_httContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        
        public string GetRoleFromJWToken() {
            return _httContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        public string GetFullNameFromJWToken()
        {
            return _httContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public string GetEmailFromJWToken()
        {
            return _httContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }
        
        public async Task<Users> GetUserByEmail(string email) 
        {
            Users user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return user;
        }
    }
}