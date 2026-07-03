using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountType Type {  get; set; }
        public decimal InitialBalance { get; set; }
        public string Currency {  get; set; }
    }
}
