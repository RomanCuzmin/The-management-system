using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Transactions;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public AccountType Type {  get; set; }
        public decimal InitialBalance { get; set; }
        public string Currency {  get; set; }
        public decimal Balance => InitialBalance + Transactions?
      .Sum(t => t.TransactionType == TransactionType.Income ? t.Amount :
                t.TransactionType == TransactionType.Expense ? -t.Amount : 0) ?? 0;
        public ICollection<Transaction> Transactions { get; set; }
        public User User { get; set; }
    }
}
