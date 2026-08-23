using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int PersonalDataId { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public PersonalData PersonalData { get; set; }
        public NotificationType NotificationType { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>(); 
        public ICollection<ReceiptItem> ReceiptItems { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<PlaceOfWork> PlaseOfWorks { get; set; } = new List<PlaceOfWork>();
        public ICollection<PayStub> PayStubs { get; set; } = new List<PayStub>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<WorkDay> WorkDays { get; set; } = new List<WorkDay>();
    }
}
