using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using Transaction = TheManagementSystem.Domain.Entities.Transaction;

namespace TheManagementSystem.Domain.Repositories
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetAll();
        public Task<Transaction> GetById(int id);
        public Task<List<Transaction>> GetByAccountId(int accountId);
        public Task<List<Transaction>> GetByCategoryId(int categoryId);
        public Task<List<Transaction>> GetByAmount(decimal  amount);
        public Task<List<Transaction>> GetByDate(DateTime date);
        public Task<List<Transaction>> GetByType(TransactionType type);
        public Task Add(Transaction transactionToAdd);
        public Task Update(Transaction transactionToUpdate);
        public Task Delete(Transaction transactionToDelete);
    }
}
