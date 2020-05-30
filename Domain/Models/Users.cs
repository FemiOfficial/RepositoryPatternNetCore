using System;

namespace repopractise.Domain.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Type { get; set; } // client or staff
        public bool IsAdmin { get; set; }
        public string ProfileImage { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Accounts Account { get; set; } = null;
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}