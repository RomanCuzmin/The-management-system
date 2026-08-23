using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IReceiptItemRepository
    {
        public Task<List<ReceiptItem>> GetAllReceiptItemsAsync();
        public Task<ReceiptItem> GetReceiptItemByIdAsync(int id);
        public Task<List<ReceiptItem>> GetReceiptItemsByTagIdAsync(int tagId);
        public Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdAsync(int receiptId);
        public Task<List<ReceiptItem>> GetReceiptItemsByNameAsync(string name);
        public Task<List<ReceiptItem>> GetReceiptItemsByQuantityAsync(decimal  quantity);
        public Task<List<ReceiptItem>> GetReceiptItemsByUnitPriceAsync(decimal totalPrice);
        public Task<List<ReceiptItem>> GetReceiptItemsByUserIdAsync(int userId);
        public Task<ReceiptItem> GetReceiptItemByIdAndUserIdAsync(int id, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByTagIdAndUserIdAsync(int tagId, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdAndUserIdAsync(int receiptId, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByNameAndUserIdAsync(string name, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByQuantityAndUserIdAsync(decimal quantity, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByUnitPriceAndUserIdAsync(decimal totalPrice, int userId);
        public Task<List<ReceiptItem>> GetAllReceiptItemsWithDetailsAsync();
        public Task<ReceiptItem> GetReceiptItemByIdWithDetailsAsync(int id);
        public Task<List<ReceiptItem>> GetReceiptItemsByTagIdWithDetailsAsync(int tagId);
        public Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdWithDetailsAsync(int receiptId);
        public Task<List<ReceiptItem>> GetReceiptItemsByNameWithDetailsAsync(string name);
        public Task<List<ReceiptItem>> GetReceiptItemsByQuantityWithDetailsAsync(decimal quantity);
        public Task<List<ReceiptItem>> GetReceiptItemsByUnitPriceWithDetailsAsync(decimal totalPrice);
        public Task<List<ReceiptItem>> GetReceiptItemsByUserIdWithDetailsAsync(int userId);
        public Task<ReceiptItem> GetReceiptItemByIdAndUserIdWithDetailsAsync(int id, int userId );
        public Task<List<ReceiptItem>> GetReceiptItemsByTagIdAndUserIdWithDetailsAsync(int tagId, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdAndUserIdWithDetailsAsync(int receiptId, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByNameAndUserIdWithDetailsAsync(string name, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByQuantityAndUserIdWithDetailsAsync(decimal quantity, int userId);
        public Task<List<ReceiptItem>> GetReceiptItemsByPricePerUnitAndUserIdWithDetailsAsync(decimal totalPrice, int userId);
        public Task AddReceiptItemAsync(ReceiptItem itemToAdd);
        public void UpdateReceiptItem(ReceiptItem itemToUpdate);
        public void DeleteReceiptItem(ReceiptItem itemToDelete);
        public Task AddReceiptItemAndSaveAsync(ReceiptItem itemToAdd);
        public Task UpdateReceiptItemAndSaveAsync(ReceiptItem itemToUpdate);
        public Task DeleteReceiptItemAndSaveAsync(ReceiptItem itemToDelete);
    }
}
//public int Id { get; set; }
//public int TagId { get; set; }
//public int UserId { get; set; }
//public int ReceiptId { get; set; }
//public string Name { get; set; }
//public decimal Quantity { get; set; }
//public decimal PricePerUnit { get; set; }
//public decimal TotalPrice => Quantity * PricePerUnit;
//public Tag Tag { get; set; }
//public User User { get; set; }
//public Receipt Receipt { get; set; }