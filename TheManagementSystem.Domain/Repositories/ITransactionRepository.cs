using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetAll();
        public Task<Transaction> GetById(int id);
        public Task<List<Transaction>> GetByAccountId(int accountId);
        public Task<List<Transaction>> GetByCategoryId(int categoryId);
        public Task<List<Transaction>> GetByAmount(decimal  amount);
        public Task<Transaction> GetByDate(DateTime date);
        public Task<List<Transaction>> GetByType(TransactionType type);
    }
}
