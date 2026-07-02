using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName {  get; set; }
        public string Patronymic {  get; set; }
        public string PlaseOfWork { get; set; }
        public  string Division {  get; set; }
        public string Post {  get; set; }
        public string ServiceNumber { get; set; }

    }
}
