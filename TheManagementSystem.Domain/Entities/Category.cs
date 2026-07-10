using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionType Type { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        public ICollection<BillingStatement> BillingStatements { get; set; } = new List<BillingStatement>();
    }      
}
