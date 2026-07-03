using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public decimal PlannedAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
