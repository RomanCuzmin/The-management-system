using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionType Type { get; set; }
    }
}
