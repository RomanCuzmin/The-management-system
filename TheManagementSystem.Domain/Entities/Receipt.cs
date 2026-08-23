using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Entities
{
    public class Receipt
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int CategoryId { get; set; }
        public DateOnly Date {  get; set; }
        public string StoreName { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public List<ReceiptItem> Items { get; set; }
    }
}
