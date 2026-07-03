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
        public string Accured {  get; set; }
        public string Withheld { get; set; }
        public string PaidOut { get; set; }
        public DateOnly BeginningOfThePeriod { get; set; }
        public DateOnly EndOfThePeriod { get; set; }
        public Worker Worker { get; set; }
        public User User { get; set; }
    }
}
