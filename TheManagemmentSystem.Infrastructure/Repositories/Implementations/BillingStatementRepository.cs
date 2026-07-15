using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public  class BillingStatementRepository : IBillingStatementRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор
        public BillingStatementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получение всех расчетных листков
        public async Task<List<BillingStatement>> CetAll() => await _context.BillingStatements.ToListAsync();

        // Получение расчетного листка по id
        public async Task<BillingStatement> GetById(int id) => await _context.BillingStatements.SingleOrDefaultAsync(x => x.Id == id);

        // Получение расчетных листков по id пользователя
        public async Task<List<BillingStatement>> GetByUserId(int userId) => await _context.BillingStatements.Where(x => x.UserId == userId).ToListAsync();

        // Получение расчетных листков по id работника
        public async Task<List<BillingStatement>> GetByWorkerId(int workerId) => await _context.BillingStatements.Where(x => x.WorkerId == workerId).ToListAsync();

        // Получение рассчетных листков по id категории  
        public async Task<List<BillingStatement>> GetByCategoryId(int categoryId) => await _context.BillingStatements.Where(x => x.CategoryId == categoryId).ToListAsync();

        // Получение рассчетных листков по id аккаунта
        public async Task<List<BillingStatement>> GetByAccountId(int accountId) => await _context.BillingStatements.Where(x.AccountId == accountId).ToListAsync();

        // Получение расчетного листка по id транзакции 
        public async Task<BillingStatement> GetByTransactionId(int transactionId) => await _context.BillingStatements.SingleOrDefaultAsync(x => x.TransactionId == transactionId); 
      
        // Добавление расчетного листка
        public async Task Add(BillingStatement billingStatementToAdd)
        {
            await _context.BillingStatements.AddAsync(billingStatementToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление расчетного листка
        public async Task Update(BillingStatement billingStatementToUpdate)
        {
            _context.BillingStatements.Update(billingStatementToUpdate);
            await _context.SaveChangesAsync();
        }

        // Удаление расчетного листка
        public async Task Delete(BillingStatement billingStatementToRemove)
        {
            _context.Remove(billingStatementToRemove);
            await _context.SaveChangesAsync();
        }
    }
}
