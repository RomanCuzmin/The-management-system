using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class PlaseOfWork
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BillingStatementId {  get; set; }
        public string Company {  get; set; }
        public string Division { get; set; }
        public string Post { get; set; }
        public string ServiceNumber { get; set; }
        public User User { get; set; }
        public ICollection<BillingStatement> BillingStatements { get; set; } = new List<BillingStatement>();
        public ICollection<WorkDay> WorkDays { get; set; } = new List<WorkDay>();
    }
}
