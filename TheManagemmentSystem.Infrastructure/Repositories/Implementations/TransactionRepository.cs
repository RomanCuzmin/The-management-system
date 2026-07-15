using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;
using Transaction = TheManagementSystem.Domain.Entities.Transaction;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        //Конструктор
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получение всех транзакций 
        public async Task<List<Transaction>> GetAll() => await _context.Transactions.ToListAsync();

        // Получение транзакции по id        
        public async Task<Transaction> GetById(int id) => await _context.Transactions.SingleOrDefaultAsync(x => x.Id == id);

        // Получение транзакций по id акаунта
        public async Task<List<Transaction>> GetByAccountId(int accountId) => await _context.Transactions.Where(x => x.AccountId == accountId).ToListAsync();

        // Получение транзакций по id категории
        public async Task<List<Transaction>> GetByCategoryId(int categoryId) => await _context.Transactions.Where(x => x.CategoryId == categoryId).ToListAsync();

        // Получение транзакций по сумме 
        public async Task<List<Transaction>> GetByAmount(decimal amount) => await _context.Transactions.Where(x =>x.Amount == amount).ToListAsync();

        // Получение транзакций по дате
        public async Task<List<Transaction>> GetByDate(DateTime date) => await _context.Transactions.Where(x => x.Date == date).ToListAsync();

        // Получение транзакций по типу
        public async Task<List<Transaction>> GetByType(TransactionType type) => await _context.Transactions.Where(x => x.TransactionType == type).ToListAsync();

        // Добавление транзакции
        public async Task Add(Transaction transactionToAdd)
        {
            await _context.Transactions.AddAsync(transactionToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление транзакции
        public async Task Update(Transaction transactionToUpdate)
        {
            _context.Transactions.Update(transactionToUpdate);
            await _context.SaveChangesAsync();
        }

        // Удаление транзакций
        public async Task Delete(Transaction transactionToDelete)
        {
            _context.Transactions.Remove(transactionToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
