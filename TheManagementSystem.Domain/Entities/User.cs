using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public NotificationType Type { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();   
    }
}
