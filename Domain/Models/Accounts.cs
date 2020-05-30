using System;

namespace repopractise.Domain.Models
{
    public class Accounts
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public float AccountBalance { get; set; }
        public float AccountOpeningBalance { get; set; }
        public string Status { get; set; }
        public EAccountType AccountType { get; set;}
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}