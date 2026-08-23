using System;
using System.Collections.Generic;
using System.Text;

namespace TheManagementSystem.Domain.Entities
{
    public class ReceiptItem
    {
        public int Id { get; set; }
        public int TagId {  get; set; }
        public int UserId { get; set; }
        public int ReceiptId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public Tag Tag { get; set; }
        public User User { get; set; }
        public Receipt Receipt { get; set; }
    }
}
