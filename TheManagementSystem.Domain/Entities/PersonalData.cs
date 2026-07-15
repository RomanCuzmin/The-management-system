using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class PersonalData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public User User { get; set; }
    }
}
