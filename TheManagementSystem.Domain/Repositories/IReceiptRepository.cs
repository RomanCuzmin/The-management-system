using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IReceiptRepository
    {
        public Task<List<Receipt>> GetAllReceiptsAsync();
        public Task<Receipt> GetReceiptByIdAsync(int id);
        public Task<List<Receipt>> GetReceiptsByCategoryIdAsync(int categoryId);
        public Task<List<Receipt>> GetReceiptsByDateAsync(DateOnly date);
        public Task<List<Receipt>> GetReceiptsByStoreNameAsync(string storeName);
        public Task<List<Receipt>> GetReceiptsByTotalAmountAsync(decimal  totalAmount);
        public Task<List<Receipt>> GetReceiptsByPaymentMethodAsync(PaymentMethod paymentMenthod);
        public Task<List<Receipt>> GetReceiptsByUserIdAsync(int userId);
        public Task<Receipt> GetReceiptByIdAndUserIdAsync(int id, int userId);
        public Task<List<Receipt>> GetReceiptsByCategoryIdAndUserIdAsync(int categoryId, int userId);
        public Task<List<Receipt>> GetReceiptsByDateAndUserIdAsync(DateOnly date, int userId);
        public Task<List<Receipt>> GetReceiptsByStoreNameAndUserIdAsync(string storeName, int userId);
        public Task<List<Receipt>> GetReceiptsByTotalAmountAndUserIdAsync(decimal totalAmount, int userId);
        public Task<List<Receipt>> GetReceiptsByPaymentMethodAndUserIdAsync(PaymentMethod paymentMenthod, int userId);
        public Task<List<Receipt>> GetAllReceiptsWithDetailsAsync();
        public Task<Receipt> GetReceiptByIdWithDetailsAsync(int id);
        public Task<List<Receipt>> GetReceiptsByCategoryIdWithDetailsAsync(int categoryId);
        public Task<List<Receipt>> GetReceiptsByDateWithDetailsAsync(DateOnly date);
        public Task<List<Receipt>> GetReceiptsByStoreNameWithDetailsAsync(string storeName);
        public Task<List<Receipt>> GetReceiptsByTotalAmountWithDetailsAsync(decimal totalAmount);
        public Task<List<Receipt>> GetReceiptsByPaymentMethodWithDetailsAsync(PaymentMethod paymentMenthod);
        public Task<List<Receipt>> GetReceiptsByUserIdWithDetailsAsync(int userId);
        public Task<Receipt> GetReceiptByIdAndUserIdWithDetailsAsync(int id, int userId);
        public Task<List<Receipt>> GetReceiptsByCategoryIdAndUserIdWithDetailsAsync(int categoryId, int userId);
        public Task<List<Receipt>> GetReceiptsByDateAndUserIdWithDetailsAsync(DateOnly date, int userId);
        public Task<List<Receipt>> GetReceiptsByStoreNameAndUserIdWithDetailsAsync(string storeName, int userId);
        public Task<List<Receipt>> GetReceiptsByTotalAmountAndUserIdWithDetailsAsync(decimal totalAmount, int userId);
        public Task<List<Receipt>> GetReceiptsByPaymentMethodAndUserIdWithDetailsAsync(PaymentMethod paymentMethod, int userId);
        public Task AddReceiptAsync(Receipt receiptToAdd);
        public void UpdateReceipt(Receipt receiptToUpdate);
        public void DeleteReceipt(Receipt receiptToRemove);
        public Task AddReceiptAndSaveAsync(Receipt receiptToAdd);
        public Task UpdateReceiptAndSaveAsync(Receipt receiptToUpdate);
        public Task DeleteReceiptAndSaveAsync(Receipt receiptToDelete);
    }
}
//public int Id { get; set; }
//public int UserId { get; set; }
//public int CategoryId { get; set; }
//public DateTime Date { get; set; }
//public string StoreName { get; set; }
//public decimal TotalAmount { get; set; }
//public PaymentMethod PaymentMethod { get; set; }
//public string? Notes { get; set; }
//public User User { get; set; }
//public Category Category { get; set; }
//public List<ReceiptItem> Items { get; set; }