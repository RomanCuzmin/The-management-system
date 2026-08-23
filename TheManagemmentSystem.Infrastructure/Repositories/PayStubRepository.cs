using Microsoft.EntityFrameworkCore;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public  class PayStubRepository : IPayStubRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор репозитория PayStubRepository
        public PayStubRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        #region Методы для получения расчетных листов
        // Асинхронный метод для получения всех расчетных листов из базы данных
        public async Task<List<PayStub>> GetAllPayStubsAsync() => await _context.PayStubs.ToListAsync();
        // Асинхронный метод для получения расчетного листа по id из базы данных
        public async Task<PayStub?> GetPayStubByIdAsync(int id) => await _context.PayStubs.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения расчетных листов по id счета из базы данных
        public async Task<List<PayStub>> GetPayStubsByAccountIdAsync(int accountId) => await _context.PayStubs.Where(x => x.AccountId == accountId).ToListAsync();
        // Асинхронный метод для получения расчетных листов по id категории из базы данных
        public async Task<List<PayStub>> GetPayStubsByCategoryIdAsync(int categoryId) => await _context.PayStubs.Where(x => x.CategoryId == categoryId).ToListAsync();
        // Асинхронный метод для получения расчетных листов по id транзакции из базы данных
        public async  Task<List<PayStub>> GetPayStubsByTransactionIdAsync(int transactionId) => await _context.PayStubs.Where(x => x.TransactionId == transactionId).ToListAsync();
        // Асинхронный метод для получения расчетных листов по id места работы из базы данных
        public async Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdAsync(int placeOfWorkId) => await _context.PayStubs.Where(x => x.PlaseOfWorkId == placeOfWorkId).ToListAsync();
        #endregion
        #region Методы для получения расчетных листков пользователя
        // Асинхронный метод для получения всех расчетных листов пользователя из базы данных
        public async Task<List<PayStub>> GetPayStubsByUserIdAsync(int userId) => await _context.PayStubs.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения расчетных листов пользователя по id из базы данных
        public async Task<PayStub?> GetPayStubByIdAndUserIdAsync(int id, int userId) => await _context.PayStubs.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения расчетных листов пользователя по статусу из базы данных
        public async Task<List<PayStub>> GetPayStubsByAccountIdAndUserIdAsync(int accountId, int userId) => await _context.PayStubs.Where(x => x.AccountId == accountId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения расчетных листов пользователя по id категории из базы данных
        public async Task<List<PayStub>> GetPayStubsByCategoryIdAndUserIdAsync(int categoryId, int userId) => await _context.PayStubs.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения расчетных листов пользователя по id транзакции из базы данных
        public async Task<List<PayStub>> GetPayStubsByTransactionIdAndUserIdAsync(int transactionId, int userId) => await _context.PayStubs.Where(x => x.TransactionId == transactionId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения расчетных листов пользователя по id места работы из базы данных
        public async Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdAndUserIdAsync(int plaseOfWorkId, int userId) => await _context.PayStubs.Where(x => x.PlaseOfWorkId == plaseOfWorkId && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для получения расчетных листов с загруженными связанными данными
        // Асинхронный метод для получения расчетных листов из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetAllPayStubsWithDetailsAsync() => await _context.PayStubs
                                                                            .Include(x => x.User)
                                                                            .Include(x => x.PlaseOfWork)
                                                                            .Include(x => x.Account)
                                                                            .Include(x => x.Category)
                                                                            .Include(x => x.Transaction)
                                                                            .ToListAsync();
        // Асинхронный метод для получения расчетного листа из базы данных по id с загруженными связанными данными
        public async Task<PayStub?> GetPayStubByIdWithDetailsAsync(int id) => await _context.PayStubs
                                                                              .Include(x => x.User)
                                                                              .Include(x => x.PlaseOfWork)
                                                                              .Include(x => x.Account)
                                                                              .Include(x => x.Category)
                                                                              .Include (x => x.Transaction)
                                                                              .SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения расчетных листов по id акаунта из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetPayStubsByAccountIdWithDetailsAsync(int accountId) => await _context.PayStubs
                                                                                                 .Include(x => x.User)
                                                                                                 .Include(x => x.PlaseOfWork)
                                                                                                 .Include(x => x.Account)
                                                                                                 .Include(x => x.Category)
                                                                                                 .Include(x => x.Transaction)
                                                                                                 .Where(x => x.AccountId == accountId)
                                                                                                 .ToListAsync();
        // Асинхронный метод для получения расчетных листов по id категории из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetPayStubsByCategoryIdWithDetailsAsync(int categoryId) => await _context.PayStubs
                                                                                                   .Include(x => x.User)
                                                                                                   .Include(x => x.PlaseOfWork)
                                                                                                   .Include(x => x.Account)
                                                                                                   .Include(x => x.Category)
                                                                                                   .Include(x => x.Transaction)
                                                                                                   .Where(x => x.CategoryId == categoryId)
                                                                                                   .ToListAsync();
        // Асинхронный метод для получения расчетных листов по id транзакции из базы данных с загруженными связанными данными
        public async Task<PayStub?> GetPayStubByTransactionIdWithDetailsAsync(int transactionId) => await _context.PayStubs
                                                                                                    .Include(x => x.User)
                                                                                                    .Include(x => x.PlaseOfWork)
                                                                                                    .Include(x => x.Account)
                                                                                                    .Include(x => x.Category)
                                                                                                    .Include(x => x.Transaction)
                                                                                                    .SingleOrDefaultAsync(x => x.TransactionId == transactionId);
        // Асинхронный метод для получения расчетных листов по id места работы из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdWithDetailsAsync(int plaseOfWorkId) => await _context.PayStubs
                                                                                                       .Include(x => x.User)
                                                                                                       .Include(x => x.PlaseOfWork)
                                                                                                       .Include(x => x.Account)
                                                                                                       .Include(x => x.Category)
                                                                                                       .Include(x => x.Transaction)
                                                                                                       .Where(x => x.PlaseOfWorkId == plaseOfWorkId)
                                                                                                       .ToListAsync();
        #endregion
        #region Методы для получения расчетных листов пользователя с загруженными связанными данными
        // Асинхронный метод для получения расчетных листов пользователя  из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetPayStubsByUserIdWithDetailsAsync(int userId) => await _context.PayStubs
                                                                                           .Include(x => x.User)
                                                                                           .Include(x => x.PlaseOfWork)
                                                                                           .Include(x => x.Account)
                                                                                           .Include(x => x.Category)
                                                                                           .Include(x => x.Transaction)
                                                                                           .Where(x => x.UserId == userId)
                                                                                           .ToListAsync();
        // Асинхронный метод для получения расчетного листа пользователя по id из базы данных с загруженными связанными данными
        public async Task<PayStub?> GetPayStubByIdAndUserIdWithDetailsAsync(int userId, int id) => await _context.PayStubs
                                                                                                   .Include(x => x.User)
                                                                                                   .Include(x => x.PlaseOfWork)
                                                                                                   .Include(x => x.Account)
                                                                                                   .Include(x => x.Category)
                                                                                                   .Include(x => x.Transaction)
                                                                                                   .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);
        // Асинхронный метод для получения расчетных листов пользователя по id акаунта из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetPayStubsByAccountIdAndUserIdWithDetailsAsync(int userId, int accountId) => await _context.PayStubs
                                                                                                             .Include(x => x.User)
                                                                                                             .Include(x => x.PlaseOfWork)
                                                                                                             .Include(x => x.Account)
                                                                                                             .Include(x => x.Category)
                                                                                                             .Include(x => x.Transaction)
                                                                                                             .Where(x => x.UserId == userId && x.AccountId == accountId)
                                                                                                             .ToListAsync();
        // Асинхронный метод для получения расчетных листов пользователя по id акаунта из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetPayStubsByCategoryIdAndUserIdWithDetailsAsync(int userId, int categoryId) => await _context.PayStubs
                                                                                                               .Include(x => x.User)
                                                                                                               .Include(x => x.PlaseOfWork)
                                                                                                               .Include(x => x.Account)
                                                                                                               .Include(x => x.Category)
                                                                                                               .Include(x => x.Transaction)
                                                                                                               .Where(x => x.UserId == userId && x.CategoryId == categoryId)
                                                                                                               .ToListAsync();
        // Асинхронный метод для получения расчетных листов пользователя по id места работы из базы данных с загруженными связанными данными
        public async Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdAndUserIdWithDetailsAsync(int userId, int plaseOfWorkId) => await _context.PayStubs
                                                                                                                     .Include(x => x.User)
                                                                                                                     .Include(x => x.PlaseOfWork)
                                                                                                                     .Include(x => x.Account)
                                                                                                                     .Include(x => x.Category)
                                                                                                                     .Include(x => x.Transaction)
                                                                                                                     .Where(x => x.UserId == userId && x.PlaseOfWorkId == plaseOfWorkId)
                                                                                                                     .ToListAsync();
        // Асинхронный метод для получения расчетных листов пользователя по id транзакции из базы данных с загруженными связанными даннными
        public async Task<PayStub?> GetPayStubByTransactionIdAndUserIdWithDetailsAsync(int userId, int transactionId) => await _context.PayStubs
                                                                                                               .Include(x => x.User)
                                                                                                               .Include(x => x.PlaseOfWork)
                                                                                                               .Include(x => x.Account)
                                                                                                               .Include(x => x.Category)
                                                                                                               .Include(x => x.Transaction)
                                                                                                               .SingleOrDefaultAsync(x => x.UserId == userId  && x.TransactionId == transactionId);
        #endregion
        #region Методы для добавления, обновления или удаления расчетного листка с сохранением или без
        // Асинхонный метод для добавления расчетного листа в базу данных без сохранения
        public async Task AddPayStubAsync(PayStub billingStatementToAdd) => await _context.PayStubs.AddAsync(billingStatementToAdd);
        // Метод для обновления расчетного листа в базе данных без сохранения
        public  void UpdatePayStub(PayStub billingStatementToUpdate) => _context.PayStubs.Update(billingStatementToUpdate);
        // Метод для удаления расчетного листа из базы данных без сохранения 
        public void DeletePayStub(PayStub billingStatementToDelete) => _context.PayStubs.Remove(billingStatementToDelete);

        // Асинхронный метод для добавления расчетного листа в базу данных с сохранением
        public async Task AddPayStubAndSaveAsync(PayStub payStubToAdd)
        {
            await _context.PayStubs.AddAsync(payStubToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления расчетного листа в базе данных с сохранением
        public async Task UpdatePayStubAndSaveAsync(PayStub payStubToUpdate)
        {
            _context.PayStubs.Update(payStubToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления расчетного листа в базе данных с сохранением 
        public async Task DeletePayStubAndSaveAsync(PayStub payStubToRemove)
        {
            _context.Remove(payStubToRemove);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
