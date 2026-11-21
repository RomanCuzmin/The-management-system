using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;
using Transaction = TheManagementSystem.Domain.Entities.Transaction;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        //Конструктор репозитория TransactionRepository
        public TransactionRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));


        #region Методы для получения транзакций
        // Асинхронный метод для получения всех транзакций
        public async Task<List<Transaction>> GetAllTransactionsAsync() => await _context.Transactions.ToListAsync();

        // Асинхронный метод для получения транзакции по id 
        public async Task<Transaction?> GetTransactionByIdAsync(int id) => await _context.Transactions.SingleOrDefaultAsync(x => x.Id == id);

        // Асинхронный метод для получения транзакций по id счета
        public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId) => await _context.Transactions.Where(x => x.AccountId == accountId).ToListAsync();

        // Асинхронный метод для получения транзакций по id категории
        public async Task<List<Transaction>> GetTransactionsByCategoryIdAsync(int categoryId) => await _context.Transactions.Where(x => x.CategoryId == categoryId).ToListAsync();

        // Асинхронный метод для получения транзакций по сумме
        public async Task<List<Transaction>> GetTransactionsByAmountAsync(decimal amount) => await _context.Transactions.Where(x =>x.Amount == amount).ToListAsync();

        // Асинхронный метод для получения транзакций по дате
        public async Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly date) => await _context.Transactions.Where(x => x.Date == date).ToListAsync();

        // Асинхронный метод для получения транзакций по типу
        public async Task<List<Transaction>> GetTransactionsByTypeAsync(TransactionType type) => await _context.Transactions.Where(x => x.TransactionType == type).ToListAsync();
        #endregion
        #region Методы для получения транзакций пользователя
        // Асинхронный метод для получения всех транзакций пользователя
        public async Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId) => await _context.Transactions.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения транзакции пользователя по id
        public async Task<Transaction?> GetTransactionByIdAndUserIdAsync(int id, int userId) => await _context.Transactions.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения транзакций пользователя по id счета
        public async Task<List<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId) => await _context.Transactions.Where(x => x.AccountId == accountId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по id категории
        public async Task<List<Transaction>> GetTransactionsByCategoryIdAndUserIdAsync(int categoryId, int userId) => await _context.Transactions.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по сумме
        public async Task<List<Transaction>> GetTransactionsByAmountAndUserIdAsync(decimal amount, int userId) => await _context.Transactions.Where(x => x.Amount ==  amount && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по дате
        public async Task<List<Transaction>> GetTransactionsByDateAndUserIdAsync(DateOnly date, int userId) => await _context.Transactions.Where(x => x.Date == date && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по типу
        public async Task<List<Transaction>> GetTransactionsByTypeAndUserIdAsync(TransactionType type, int userId) => await _context.Transactions.Where(x => x.TransactionType == type && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для получения транзакций с загруженными связанными данными
        // Асинхронный метод для получения транзакций с загруженными связанными данными
        public async Task<List<Transaction>> GetAllTransactionsWithDetailsAsync() => await _context.Transactions
                                                                                  .Include(x => x.User)
                                                                                  .Include(x => x.ToUser)
                                                                                  .Include(x => x.Account) 
                                                                                  .Include(x => x.ToAccount)
                                                                                  .Include (x => x.PayStubs)
                                                                                  .ToListAsync();
        // Асинхронный метод для получения транзакции по id с загруженными связанными данными
        public async Task<Transaction?> GetTransactionByIdWithDetailsAsync(int id) => await _context.Transactions
                                                                                   .Include(x => x.User)
                                                                                   .Include(x => x.ToUser)
                                                                                   .Include(x => x.Account)
                                                                                   .Include(x => x.PayStubs)
                                                                                   .SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения транзакций по id счета с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByAccountIdWithDetailsAsync(int accountId) => await _context.Transactions
                                                                                                       .Include(x => x.User)
                                                                                                       .Include(x => x.ToUser)
                                                                                                       .Include(x => x.Account)
                                                                                                       .Include(x => x.ToAccount)
                                                                                                       .Include(x => x.PayStubs)
                                                                                                       .Where(x => x.AccountId == accountId)
                                                                                                       .ToListAsync();
        // Асинхронный метод для получения транзакций по id категории с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByCategoryIdWithDetailsAsync(int categoryId) => await _context.Transactions
                                                                                                         .Include(x => x.User)
                                                                                                         .Include(x => x.ToUser)
                                                                                                         .Include(x => x.Account)
                                                                                                         .Include(x => x.ToAccount)
                                                                                                         .Include(x => x.PayStubs)
                                                                                                         .Where(x => x.CategoryId == categoryId)
                                                                                                         .ToListAsync();
        // Асинхронный метод для получения транзакций по id счета с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByAmountWithDetailsAsync(decimal amount) => await _context.Transactions
                                                                                                     .Include(x => x.User)
                                                                                                     .Include(x => x.ToUser)
                                                                                                     .Include(x => x.Account)
                                                                                                     .Include(x => x.ToAccount)
                                                                                                     .Include(x => x.PayStubs)
                                                                                                     .Where(x => x.Amount == amount)
                                                                                                     .ToListAsync();
        // Асинхронный метод для получения транзакций по дате с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByDateWithDetailsAsync(DateOnly date) => await _context.Transactions
                                                                                                  .Include(x => x.User)
                                                                                                  .Include(x => x.ToUser)
                                                                                                  .Include(x => x.Account)
                                                                                                  .Include(x => x.ToAccount)
                                                                                                  .Include(x => x.PayStubs)
                                                                                                  .Where(x => x.Date == date)
                                                                                                  .ToListAsync();
        // Асинхронный метод для получения транзакций по типу с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByTypeWithDetailsAsync(TransactionType type) => await _context.Transactions
                                                                                                         .Include(x => x.User)
                                                                                                         .Include(x => x.ToUser)
                                                                                                         .Include(x => x.Account)
                                                                                                         .Include(x => x.ToAccount)
                                                                                                         .Include(x => x.PayStubs)
                                                                                                         .Where(x => x.TransactionType == type)
                                                                                                         .ToListAsync();
        #endregion
        #region Методы для получения транзакций пользователя с загруженными связанными данными
        // Асинхронный метод для получения транзакций пользователя с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByUserIdWithDetailsAsync(int userId) => await _context.Transactions
                                                                                                 .Include(x => x.Account)
                                                                                                 .Include(x => x.ToAccount)
                                                                                                 .Include(x => x.PayStubs)
                                                                                                 .Where(x => x.UserId == userId)
                                                                                                 .ToListAsync();
        // Асинхронный метод для получения транзакции пользователя по id с загруженными связанными данными
        public async Task<Transaction?> GetTransactionByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.Transactions
                                                                                                        .Include(x => x.Account)
                                                                                                        .Include(x => x.ToAccount)
                                                                                                        .Include(x => x.PayStubs)
                                                                                                        .SingleOrDefaultAsync(x => x.Id == id  && x.UserId == userId);
        // Асинхронный метод для получения транзакций пользователя по id акаунта с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByAccountIdAndUserIdWithDetailsAsync(int accountId, int userId) => await _context.Transactions
                                                                                                                            .Include(x => x.Account)
                                                                                                                            .Include(x => x.ToAccount)
                                                                                                                            .Include(x => x.PayStubs)
                                                                                                                            .Where(x => x.AccountId == accountId && x.UserId == userId)
                                                                                                                            .ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по id категории с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByCategoryIdAndUserIdWithDetailsAsync(int categoryId, int userId) => await _context.Transactions
                                                                                                                              .Include(x => x.Account)
                                                                                                                              .Include(x => x.ToAccount)
                                                                                                                              .Include(x => x.PayStubs)
                                                                                                                              .Where(x => x.CategoryId == categoryId && x.UserId == userId)
                                                                                                                              .ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по сумме с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByAmountAndUserIdWithDetailsAsync(decimal amount, int userId) => await _context.Transactions
                                                                                                                          .Include(x => x.Account)
                                                                                                                          .Include(x => x.ToAccount)
                                                                                                                          .Include(x => x.PayStubs)
                                                                                                                          .Where(x => x.Amount == amount && x.UserId == userId)
                                                                                                                          .ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по дате с загруженными связанными данными
        public async Task<List<Transaction>> GetTransactionsByDateAndUserIdWithDetailsAsync(DateOnly date, int userId) => await _context.Transactions
                                                                                                                       .Include(x => x.Account)
                                                                                                                       .Include(x => x.ToAccount)
                                                                                                                       .Include(x => x.PayStubs)
                                                                                                                       .Where(x => x.Date == date && x.UserId == userId)
                                                                                                                       .ToListAsync();
        // Асинхронный метод для получения транзакций пользователя по типу с загружеными связанными данными
        public async Task<List<Transaction>> GetTransactionsByTypeAndUserIdWithDetailsAsync(TransactionType type, int userId) => await _context.Transactions
                                                                                                                               .Include(x => x.Account)
                                                                                                                               .Include(x => x.ToAccount)
                                                                                                                               .Include(x => x.PayStubs)
                                                                                                                               .Where(x => x.TransactionType == type && x.UserId == userId)
                                                                                                                               .ToListAsync();
        #endregion
        #region Методы для добавления, обновления или удаления транзакции с сохранением или без
        // Асинхронный метод для добавления транзакции без сохранения
        public async Task AddTransactionAsync(Transaction transactionToAdd) => await _context.Transactions.AddAsync(transactionToAdd);
        // Метод для обновления транзакции без сохранения
        public void UpdateTransaction(Transaction transactionToUpdate) => _context.Transactions.Update(transactionToUpdate);
        // Метод для удаления транзакции
        public void DeleteTransaction(Transaction transactionToDelete) => _context.Transactions.Remove(transactionToDelete);
 
        // Асинхронный метод для добавления транзакции с сохранением
        public async Task AddTransactionAndSaveAsync(Transaction transactionToAdd)
        {
            await _context.Transactions.AddAsync(transactionToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления транзакции с сохранением
        public async Task UpdateTransactionAndSaveAsync(Transaction transactionToUpdate)
        {
            _context.Transactions.Update(transactionToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления транзакции с сохранением
        public async Task DeleteTransactionAndSaveAsync(Transaction transactionToDelete)
        {
            _context.Transactions.Remove(transactionToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
