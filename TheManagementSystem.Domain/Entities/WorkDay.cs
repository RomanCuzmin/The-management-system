using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class WorkDay
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int BillingStatementId {  get; set; }
        public DateTime Date { get; set; }
        public short HourlyRate { get; set; }
        public short TotalWorkHours { get; set; }
        public short BreakHours { get; set; }
        public int PaidHours => TotalWorkHours - BreakHours;
        public decimal GrossEarnings => PaidHours * HourlyRate;
        public decimal TaxRate { get; set; } = 0.13m;
        public decimal TaxAmount => GrossEarnings * TaxRate;
        public decimal NetEarnings => GrossEarnings - TaxAmount;
        public Worker Worker { get; set; }
        public BillingStatement BillingStatement {  get; set; }
    }
}
