using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class ReceiptItemRepository : IReceiptItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ReceiptItemRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        #region Методы для получения состовляющих чеков
        // Асинхронный метод для получения всех составляющих чека
        public async Task<List<ReceiptItem>> GetAllReceiptItemsAsync() => await _context.ReceiptItems.ToListAsync();
        // Асинхронный метод для получения состовляющей чека по id
        public async Task<ReceiptItem?> GetReceiptItemByIdAsync(int id) => await _context.ReceiptItems.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения состовляющих чеков по id тэга
        public async Task<List<ReceiptItem>> GetReceiptItemsByTagIdAsync(int tagId) => await _context.ReceiptItems.Where(x => x.TagId == tagId).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков по id чека
        public async Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdAsync(int receiptId) => await _context.ReceiptItems.Where(x => x.ReceiptId == receiptId).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков по имени 
        public async Task<List<ReceiptItem>> GetReceiptItemsByNameAsync(string name) => await _context.ReceiptItems.Where(x => x.Name == name).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков по цене за еденицу
        public async Task<List<ReceiptItem>> GetReceiptItemsByUnitPriceAsync(decimal unitPrice) => await _context.ReceiptItems.Where(x => x.UnitPrice == unitPrice).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков по количеству
        public async Task<List<ReceiptItem>> GetReceiptItemsByQuantityAsync(decimal quantity) => await _context.ReceiptItems.Where(x => x.Quantity == quantity).ToListAsync();
        #endregion
        #region Методы для получения состовляющих чеков пользователя
        // Асинхронный метод для получения всех состовляющих чеков пользователя
        public async Task<List<ReceiptItem>> GetReceiptItemsByUserIdAsync(int userId) => await _context.ReceiptItems.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения состовляющей чека пользователя по id 
        public async Task<ReceiptItem?> GetReceiptItemByIdAndUserIdAsync(int id, int userId) => await _context.ReceiptItems.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения состовляющих чеков пользователя по id тэга
        public async Task<List<ReceiptItem>> GetReceiptItemsByTagIdAndUserIdAsync(int tagId, int  userId) => await _context.ReceiptItems.Where(x => x.TagId == tagId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков пользователя по id чека
        public async Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdAndUserIdAsync(int receiptId, int userId) => await _context.ReceiptItems.Where(x => x.ReceiptId == receiptId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков пользователя по именни 
        public async Task<List<ReceiptItem>> GetReceiptItemsByNameAndUserIdAsync(string name, int userId) => await _context.ReceiptItems.Where(x => x.Name == name && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков пользователя по цене за еденицу
        public async Task<List<ReceiptItem>> GetReceiptItemsByUnitPriceAndUserIdAsync(decimal unitPrice, int userId) => await _context.ReceiptItems.Where(x => x.UnitPrice == unitPrice && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения состовляющих чеков пользователя по колиству
        public async Task<List<ReceiptItem>> GetReceiptItemsByQuantityAndUserIdAsync(decimal quantity, int userId) => await _context.ReceiptItems.Where(x => x.Quantity == quantity && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для получения составляющих чеков с загруженными связанными данными
        // Асинхронный метод для получения всех состовляющих чеков с загруженными связанными данными
        public async Task<List<ReceiptItem>> GetAllReceiptItemsWithDetailsAsync() => await _context.ReceiptItems
                                                                                                   .Include(x => x.Tag)
                                                                                                   .Include(x => x.Receipt)
                                                                                                   .ToListAsync();
        // Асинхронный метод для получения состовляющей чека по id с загруженными связанными данными
        public async Task<ReceiptItem?> GetReceiptItemByIdWithDetailsAsync(int id) => await _context.ReceiptItems
                                                                                                    .Include(x => x.Tag)
                                                                                                    .Include(x => x.Receipt)
                                                                                                    .SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения состовляюших чеков по id тэга с загруженными связанными данными
        public async Task<List<ReceiptItem>> GetReceiptItemsByTagIdWithDetailsAsync(int tagId) => await _context.ReceiptItems
                                                                                                                .Include(x => x.Tag)
                                                                                                                .Include(x => x.Receipt)
                                                                                                                .Where(x => x.TagId == tagId)
                                                                                                                .ToListAsync();
        // Асинхронный метод для получения состовляющих чеков по id чека с загруженными связанными данными
        public async Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdWithDetailsAsync(int receiptId) => await _context.ReceiptItems
                                                                                                                        .Include(x => x.Tag)
                                                                                                                        .Include(x => x.Receipt)
                                                                                                                        .Where(x => x.ReceiptId == receiptId)
                                                                                                                        .ToListAsync();
        // Асинхронный метод для получения составляющих чеков по именни с загруженными связанными данными
        public async Task<List<ReceiptItem>> GetReceiptItemsByNameWithDetailsAsync(string name) => await _context.ReceiptItems
                                                                                                                 .Include(x => x.Tag)
                                                                                                                 .Include(x => x.Receipt)
                                                                                                                 .Where(x => x.Name == name)
                                                                                                                 .ToListAsync();
        // Асинхронный метод для получения составляющих чеков по цене за еденицу с загруженными связанными данными
        public async Task<List<ReceiptItem>> GetReceiptItemsByUnitPriceWithDetailsAsync(decimal unitPrice) => await _context.ReceiptItems
                                                                                                                            .Include(x => x.Tag)
                                                                                                                            .Include(x => x.Receipt)
                                                                                                                            .Where(x => x.UnitPrice == unitPrice)
                                                                                                                            .ToListAsync();
        // Асинхронный метод для получения состовляющих чеков по количеству с загруженными свяязанными данными
        public async Task<List<ReceiptItem>> GetReceiptItemsByQuantityWithDetailsAsync(decimal quantity) => await _context.ReceiptItems
                                                                                                                          .Include(x => x.Tag)
                                                                                                                          .Include(x => x.Receipt)
                                                                                                                          .Where(x => x.Quantity == quantity)
                                                                                                                          .ToListAsync();
        #endregion
        #region Методы для получения составляющих чеков пользователя с загруженными связанными данными
        // Асинхронный метод для получения всех состовляющих чеков пользователя
        public async Task<List<ReceiptItem>> GetReceiptItemsByUserIdWithDetailsAsync(int userId) => await _context.ReceiptItems
                                                                                                                  .Include(x => x.Tag)
                                                                                                                  .Include(x => x.Receipt)
                                                                                                                  .Where(x => x.UserId == userId)
                                                                                                                  .ToListAsync();
        // Асинхронный метод для получения состовляющей чеков пользователя по id
        public async Task<ReceiptItem?> GetReceiptItemByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.ReceiptItems
                                                                                                                         .Include(x => x.Tag)
                                                                                                                         .Include(x => x.Receipt)
                                                                                                                         .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения состовляющей чеков пользователя по id тэга
        public async Task<List<ReceiptItem>> GetReceiptItemsByTagIdAndUserIdWithDetailsAsync(int tagId, int userId) => await _context.ReceiptItems
                                                                                                                                     .Include(x => x.Tag)
                                                                                                                                     .Include(x => x.Receipt)
                                                                                                                                     .Where(x => x.TagId == tagId && x.UserId == userId)
                                                                                                                                     .ToListAsync();
        // Асинхронный метод для получения составляющих чеков пользователя по id чека
        public async Task<List<ReceiptItem>> GetReceiptItemsByReceiptIdAndUserIdWithDetailsAsync(int receiptId, int userId) => await _context.ReceiptItems
                                                                                                                                             .Include(x => x.Tag)
                                                                                                                                             .Include(x => x.Receipt)
                                                                                                                                             .Where(x => x.ReceiptId == receiptId && x.UserId == userId)
                                                                                                                                             .ToListAsync();
        // Асинхронный метод для получения состовляющих чеков пользователя по именни
        public async Task<List<ReceiptItem>> GetReceiptItemsByNameAndUserIdWithDetailsAsync(string name, int userId) => await _context.ReceiptItems
                                                                                                                                      .Include(x => x.Tag)
                                                                                                                                      .Include(x => x.Receipt)
                                                                                                                                      .Where(x => x.Name == name && x.UserId == userId)
                                                                                                                                      .ToListAsync();
        // Асинхронный метод для получения состовляющих чеков пользователя по цене за еденицу
        public async Task<List<ReceiptItem>> GetReceiptItemsByPricePerUnitAndUserIdWithDetailsAsync(decimal unitPrice, int userId) => await _context.ReceiptItems
                                                                                                                                                    .Include(x => x.Tag)
                                                                                                                                                    .Include(x => x.Receipt)
                                                                                                                                                    .Where(x => x.UnitPrice == unitPrice && x.UserId == userId)
                                                                                                                                                    .ToListAsync();
        // Асинхронный метод для получения составляющих чеков пользователя по колличеству
        public async Task<List<ReceiptItem>> GetReceiptItemsByQuantityAndUserIdWithDetailsAsync(decimal quantity, int userId) => await _context.ReceiptItems
                                                                                                                                               .Include(x => x.Tag)
                                                                                                                                               .Include(x => x.Receipt)
                                                                                                                                               .Where(x => x.Quantity == quantity && x.UserId == userId)
                                                                                                                                               .ToListAsync();
        #endregion
        #region Методы для добавления, обновления или удаления состовляющей чека с сохранением или без
        // Асинхронный метод для добавления составляющей чека без сохранения
        public async Task AddReceiptItemAsync(ReceiptItem itemToAdd) => await _context.ReceiptItems.AddAsync(itemToAdd);
        // Метод для обновления составляющей чека без сохранения
        public void UpdateReceiptItem(ReceiptItem itemToUpdate) => _context.ReceiptItems.Update(itemToUpdate);
        // Метод для удаления составляющей чека без сохранения
        public void DeleteReceiptItem(ReceiptItem itemToDelete) => _context.ReceiptItems.Remove(itemToDelete);

        // Асинхронный метод для добавления составляющей чека с сохранением
        public async Task AddReceiptItemAndSaveAsync(ReceiptItem itemToAdd)
        {
            await _context.ReceiptItems.AddAsync(itemToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления составляющей чека с сохранением
        public async Task UpdateReceiptItemAndSaveAsync(ReceiptItem itemToUpdate)
        {
            _context.ReceiptItems.Update(itemToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления составляющей чека с сохранением
        public async Task DeleteReceiptItemAndSaveAsync(ReceiptItem itemToDelete)
        {
            _context.ReceiptItems.Remove(itemToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
