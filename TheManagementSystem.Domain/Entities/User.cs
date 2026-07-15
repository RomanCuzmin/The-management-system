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
        public ICollection<PlaseOfWork> PlaseOfWorks { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        public ICollection<BillingStatement> BillingStatements { get; set; } = new List<BillingStatement>();
    }
}
