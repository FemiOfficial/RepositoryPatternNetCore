using System;

namespace repopractise.Domain.Dtos.Transactions
{
    public class TransactionResponseDto
    {
        public string TransactionReference { get; set; }
        public string RecipientAccountNumber { get; set; }
        public string SenderAccountNumber { get; set; }
        public float Amount { get; set; }
        public string TransactionType { get; set; }
        public int Cashier { get; set; }
        public DateTime TransanctionTime { get; set; }

    }
}