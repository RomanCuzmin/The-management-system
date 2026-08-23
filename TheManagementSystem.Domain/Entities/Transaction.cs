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
        public DateOnly Date {  get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public int UserId { get; set; }
        public int ToUserId { get; set; }
        public int AccountId { get; set; } 
        public int? ToAccountId { get; set; }      
        public int? CategoryId { get; set; }
        public User User { get; set; }
        public User ToUser { get; set; }
        public Account Account { get; set; }
        public Account ToAccount { get; set; }
        public Category Category { get; set; }
        public ICollection<PayStub> PayStubs { get; set; } = new List<PayStub>();
    }
}
