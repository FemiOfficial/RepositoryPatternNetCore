using repopractise.Domain.Models;

namespace repopractise.Domain.Dtos.Accounts
{
    public class CreateAccountRequestDto
    {
        public float AccountOpeningBalance { get; set; }
        public string Status { get; set; } // active || inactive
        
        public EAccountType AccountType { get; set; }
    }
}