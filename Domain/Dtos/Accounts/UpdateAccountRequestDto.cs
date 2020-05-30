using repopractise.Domain.Models;

namespace repopractise.Domain.Dtos.Accounts
{
    public class UpdateAccountRequestDto
    {
        public string Status { get; set; } // active || inactive
        public EAccountType AccountType { get; set; }
        public string AccountNumber { get; set; }
        public float AccountBalance { get; set; }
    }
}
