using System;

namespace repopractise.Domain.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public string TransactionReference { get; set; }
        public string RecipientAccountNumber { get; set; }
        public string SenderAccountNumber { get; set; }
        public float Amount { get; set; }
        public ETransactionType TransactionType { get; set; }
        public int Cashier { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}