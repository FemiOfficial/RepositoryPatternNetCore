using repopractise.Domain.Models;

namespace repopractise.Domain.Dtos.Accounts
{
    public class CreateAccountResponseDto
    {
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        //public Users User { get; set; }
        public float AccountBalance { get; set; }
        public float AccountOpeningBalance { get; set; }
        public string Status { get; set; }
        public EAccountType AccountType { get; set; }
    }
}