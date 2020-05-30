using repopractise.Domain.Models;

namespace repopractise.Domain.Dtos.Accounts
{
    public class UpdateAccountResponseDto
    {
        public string Status { get; set; } // active || inactive
        public string AccountNumber { get; set; } // active || inactive
        public EAccountType AccountType { get; set; }
        public float AccountBalance { get; set; }
    }
}