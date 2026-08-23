using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class PlaceOfWork
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Company {  get; set; }
        public string Division { get; set; }
        public string Post { get; set; }
        public string ServiceNumber { get; set; }
        public User User { get; set; }
        public ICollection<PayStub> PayStubs { get; set; } = new List<PayStub>();
        public ICollection<WorkDay> WorkDays { get; set; } = new List<WorkDay>();
    }
}
