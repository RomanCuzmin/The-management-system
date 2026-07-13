using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public int AccountId { get; set; } 
        public int? ToAccountId { get; set; }      
        public int? CategoryId { get; set; }
        public Account Account { get; set; }
        public Account ToAccount { get; set; }
        public Category Category { get; set; }
        public ICollection<BillingStatement> BillingStatements { get; set; } = new List<BillingStatement>();
    }
}
