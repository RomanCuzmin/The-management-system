using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class BillingStatement
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public int CategoryId {  get; set; }
        public int TransactionId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal OtherAccruals { get; set; }
        public decimal TotalAccrued => BaseSalary + Bonus + OtherAccruals;
        public decimal IncomeTax { get; set; }
        public decimal PensionContribution { get; set; }
        public decimal OtherDeductions { get; set; }
        public decimal TotalDeducted => IncomeTax + PensionContribution + OtherDeductions;
        public decimal NetAmount => TotalAccrued - TotalDeducted;
        public Worker Worker { get; set; }
        public User User { get; set; }
    }
}
