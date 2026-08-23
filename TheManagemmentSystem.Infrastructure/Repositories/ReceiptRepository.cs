using Microsoft.EntityFrameworkCore;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        // Конструктор репозитория ReceiptRepository
        public ReceiptRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        #region Методы для получения чеков
        // Асинхронный метод для получения всех чеков
        public async Task<List<Receipt>> GetAllReceiptsAsync() => await _context.Receipts.ToListAsync();
        // Асинхронный метод для получения чека по id
        public async Task<Receipt?> GetReceiptByIdAsync(int id) => await _context.Receipts.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения чеков по id категории 
        public async Task<List<Receipt>> GetReceiptsByCategoryIdAsync(int categoryId) => await _context.Receipts.Where(x => x.CategoryId == categoryId).ToListAsync();
        // Асинхронный метод для получения чеков по дате 
        public async Task<List<Receipt>> GetReceiptsByDateAsync(DateOnly date) => await _context.Receipts.Where(x=> x.Date == date).ToListAsync();
        // Асинхронный метод для получения чеков по способу оплаты
        public async Task<List<Receipt>> GetReceiptsByPaymentMethodAsync(PaymentMethod paymentMenthod) => await _context.Receipts.Where(x => x.PaymentMethod == paymentMenthod).ToListAsync();
        // Асинхронный метод для получения чеков по именни магазина
        public async Task<List<Receipt>> GetReceiptsByStoreNameAsync(string storeName) => await _context.Receipts.Where(x => x.StoreName == storeName).ToListAsync();
        // Асинхронный метод для получения чеков по сумме
        public async Task<List<Receipt>> GetReceiptsByTotalAmountAsync(decimal totalAmount) => await _context.Receipts.Where(x => x.TotalAmount == totalAmount).ToListAsync();
        #endregion
        #region Методы для получения чеков пользователя
        // Асинхронный метод для получения всех чеков пользователя
        public async Task<List<Receipt>> GetReceiptsByUserIdAsync(int userId) => await _context.Receipts.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения чеков пользователя по id
        public async Task<Receipt?> GetReceiptByIdAndUserIdAsync(int id, int userId) => await _context.Receipts.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения чеков пользователя по id категории
        public async Task<List<Receipt>> GetReceiptsByCategoryIdAndUserIdAsync(int categoryId, int userId) => await _context.Receipts.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения чеков пользователя по дате
        public async Task<List<Receipt>> GetReceiptsByDateAndUserIdAsync(DateOnly date, int userId) => await _context.Receipts.Where(x => x.Date == date && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения чеков пользователя по способу оплаты
        public async Task<List<Receipt>> GetReceiptsByPaymentMethodAndUserIdAsync(PaymentMethod paymentMenthod, int userId) => await _context.Receipts.Where(x => x.PaymentMethod == paymentMenthod && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения чеков пользователя по именни магазина
        public async Task<List<Receipt>> GetReceiptsByStoreNameAndUserIdAsync(string storeName, int userId) => await _context.Receipts.Where(x => x.StoreName == storeName && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения чеков пользователя по сумме
        public async Task<List<Receipt>> GetReceiptsByTotalAmountAndUserIdAsync(decimal totalAmount, int userId) => await _context.Receipts.Where(x => x.TotalAmount == totalAmount && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для получения чеков с загруженными связанными данными
        // Асинхронный метод для получения всех чеков с загруженными связанными данными
        public async Task<List<Receipt>> GetAllReceiptsWithDetailsAsync() => await _context.Receipts
                                                                                           .Include(x => x.Category)
                                                                                           .Include(x => x.Items)
                                                                                           .Include(x => x.User)
                                                                                           .ToListAsync();
        // Асинхронный метод для получения чека по id с загруженными связанными данными
        public async Task<Receipt?> GetReceiptByIdWithDetailsAsync(int id) => await _context.Receipts
                                                                                            .Include(x => x.Category)
                                                                                            .Include(x => x.Items)
                                                                                            .Include(x => x.User)
                                                                                            .SingleOrDefaultAsync(x => x.Id == id);                                        
        // Асинхронный метод для получения чеков по id категории  с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByCategoryIdWithDetailsAsync(int categoryId) => await _context.Receipts
                                                                                                                  .Include(x => x.Category)
                                                                                                                  .Include(x => x.Items)
                                                                                                                  .Include(x => x.User)
                                                                                                                  .Where(x => x.CategoryId == categoryId)
                                                                                                                  .ToListAsync();
        // Асинхронный метод для получения чеков по дате с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByDateWithDetailsAsync(DateOnly date) => await _context.Receipts
                                                                                                           .Include(x => x.Category)
                                                                                                           .Include(x => x.Items)
                                                                                                           .Include(x => x.User)
                                                                                                           .Where(x => x.Date == date)
                                                                                                           .ToListAsync(); 
        // Асинхронный метод для получения чеков по методу оплаты с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByPaymentMethodWithDetailsAsync(PaymentMethod paymentMenthod) => await _context.Receipts
                                                                                                                                   .Include(x => x.Category)
                                                                                                                                   .Include(x => x.Items)
                                                                                                                                   .Include(x => x.User)
                                                                                                                                   .Where(x => x.PaymentMethod == paymentMenthod)
                                                                                                                                   .ToListAsync();
        // Асинхронный метод для получения чеков по именни магазина с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByStoreNameWithDetailsAsync(string storeName) => await _context.Receipts
                                                                                                                   .Include(x => x.Category)
                                                                                                                   .Include(x => x.Items)
                                                                                                                   .Include(x => x.User)
                                                                                                                   .Where(x => x.StoreName == storeName)
                                                                                                                   .ToListAsync();
        // Асинхронный метод для получения чеков по сумме с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByTotalAmountWithDetailsAsync(decimal totalAmount) => await _context.Receipts
                                                                                                                        .Include(x => x.Category)
                                                                                                                        .Include(x => x.Items)
                                                                                                                        .Where(x => x.TotalAmount == totalAmount)
                                                                                                                        .ToListAsync();
        #endregion
        #region Методы для получения чеков пользователя с загруженными связанными данными
        // Асинхронный метод  для получения всех чеков пользователя с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByUserIdWithDetailsAsync(int userId) => await _context.Receipts
                                                                                                          .Include(x => x.Category)
                                                                                                          .Include (x => x.Items)
                                                                                                          .Where(x => x.UserId == userId)
                                                                                                          .ToListAsync();
        // Асинхронный метод для получения чеков пользователя по id с загруженными связанными данными
        public async Task<Receipt?> GetReceiptByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.Receipts
                                                                                                                 .Include(x => x.Category)
                                                                                                                 .Include(x => x.Items)
                                                                                                                 .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения чеков пользователя по id категории с загрженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByCategoryIdAndUserIdWithDetailsAsync(int categoryId, int userId) => await _context.Receipts
                                                                                                                                       .Include(x => x.Category)
                                                                                                                                       .Include(x => x.Items)
                                                                                                                                       .Where(x => x.CategoryId == categoryId && x.UserId == userId)
                                                                                                                                       .ToListAsync();
        // Асинхронный метод для получения чеков пользователя по дате с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByDateAndUserIdWithDetailsAsync(DateOnly date, int userId) => await _context.Receipts
                                                                                                                                .Include(x => x.Category)
                                                                                                                                .Include(x => x.Items)
                                                                                                                                .Where(x => x.Date == date && x.UserId == userId)
                                                                                                                                .ToListAsync();
        // Асинхронный метод для получения  чеков пользователя по способу оплаты с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByPaymentMethodAndUserIdWithDetailsAsync(PaymentMethod paymentMenthod, int userId) => await _context.Receipts
                                                                                                                                                        .Include(x => x.Category)
                                                                                                                                                        .Include(x => x.Items)
                                                                                                                                                        .Where(x => x.PaymentMethod == paymentMenthod && x.UserId == userId)
                                                                                                                                                        .ToListAsync();
        // Асинхронный метод для получения чеков пользователя по именни магазина с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByStoreNameAndUserIdWithDetailsAsync(string storeName, int userId) => await _context.Receipts
                                                                                                                                        .Include(x => x.Category)
                                                                                                                                        .Include(x => x.Items)
                                                                                                                                        .Where(x => x.StoreName == storeName && x.UserId == userId)
                                                                                                                                        .ToListAsync();
        // Асинхронный метод для получения чеков по сумме с загруженными связанными данными
        public async Task<List<Receipt>> GetReceiptsByTotalAmountAndUserIdWithDetailsAsync(decimal totalAmount, int userId) => await _context.Receipts
                                                                                                                                             .Include(x => x.Category)
                                                                                                                                             .Include(x => x.Items)
                                                                                                                                             .Where(x => x.TotalAmount == totalAmount && x.UserId == userId)
                                                                                                                                             .ToListAsync();
        #endregion
        #region Методы для добавления, обновления или удаления чека с сохранением или без
        // Асинхронный метод для добавления чека без сохранения
        public async Task AddReceiptAsync(Receipt receiptToAdd) => await _context.Receipts.AddAsync(receiptToAdd);
        // Метод для обновления чека без сохранения
        public void UpdateReceipt(Receipt receiptToUpdate) => _context.Receipts.Update(receiptToUpdate);
        // Метод для удаления чека без сохранения
        public void DeleteReceipt(Receipt receiptToRemove) => _context.Receipts.Remove(receiptToRemove);

        // Асинхронный метод для добавления чека с сохранением
        public async Task AddReceiptAndSaveAsync(Receipt receiptToAdd)
        {
            await _context.Receipts.AddAsync(receiptToAdd);
            await _context.SaveChangesAsync();
        }

        // Асихронный метод для обновления чека с сохранением
        public async Task UpdateReceiptAndSaveAsync(Receipt receiptToUpdate)
        {
            _context.Receipts.Update(receiptToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления чека c сохранением
        public async Task DeleteReceiptAndSaveAsync(Receipt receiptToDelete)
        {
            _context.Receipts.Remove(receiptToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
