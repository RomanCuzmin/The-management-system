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
        public Task<List<Transaction>> GetAllTransactionsAsync();
        public Task<Transaction?> GetTransactionByIdAsync(int id);
        public Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        public Task<List<Transaction>> GetTransactionsByCategoryIdAsync(int categoryId);
        public Task<List<Transaction>> GetTransactionsByAmountAsync(decimal  amount);
        public Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly date);
        public Task<List<Transaction>> GetTransactionsByTypeAsync(TransactionType type);
        public Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId);
        public Task<Transaction?> GetTransactionByIdAndUserIdAsync(int id, int userId);
        public Task<List<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId);
        public Task<List<Transaction>> GetTransactionsByCategoryIdAndUserIdAsync(int categoryId, int userId);
        public Task<List<Transaction>> GetTransactionsByAmountAndUserIdAsync(decimal amount, int userId);
        public Task<List<Transaction>> GetTransactionsByDateAndUserIdAsync(DateOnly date, int userId);
        public Task<List<Transaction>> GetTransactionsByTypeAndUserIdAsync(TransactionType type, int userId);
        public Task<List<Transaction>> GetAllTransactionsWithDetailsAsync();
        public Task<Transaction?> GetTransactionByIdWithDetailsAsync(int id);
        public Task<List<Transaction>> GetTransactionsByAccountIdWithDetailsAsync(int accountId);
        public Task<List<Transaction>> GetTransactionsByCategoryIdWithDetailsAsync(int categoryId);
        public Task<List<Transaction>> GetTransactionsByAmountWithDetailsAsync(decimal amount);
        public Task<List<Transaction>> GetTransactionsByDateWithDetailsAsync(DateOnly date);
        public Task<List<Transaction>> GetTransactionsByTypeWithDetailsAsync(TransactionType type);
        public Task<List<Transaction>> GetTransactionsByUserIdWithDetailsAsync(int userId);
        public Task<Transaction?> GetTransactionByIdAndUserIdWithDetailsAsync(int id, int userId);
        public Task<List<Transaction>> GetTransactionsByAccountIdAndUserIdWithDetailsAsync(int accountId, int userId);
        public Task<List<Transaction>> GetTransactionsByCategoryIdAndUserIdWithDetailsAsync(int categoryId, int userId);
        public Task<List<Transaction>> GetTransactionsByAmountAndUserIdWithDetailsAsync(decimal amount, int userId);
        public Task<List<Transaction>> GetTransactionsByDateAndUserIdWithDetailsAsync(DateOnly date, int userId);
        public Task<List<Transaction>> GetTransactionsByTypeAndUserIdWithDetailsAsync(TransactionType type, int userId);
        public Task AddTransactionAsync(Transaction transactionToAdd);
        public void UpdateTransaction(Transaction transactionToUpdate);
        public void DeleteTransaction(Transaction transactionToDelete);
        public Task AddTransactionAndSaveAsync(Transaction transactionToAdd);
        public Task UpdateTransactionAndSaveAsync(Transaction transactionToUpdate);
        public Task DeleteTransactionAndSaveAsync(Transaction transactionToDelete);
    }
}
