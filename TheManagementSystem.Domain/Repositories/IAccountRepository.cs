using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IAccountRepository
    {
        public Task<List<Account>> GetAllAccountsAsync();
        public Task<Account> GetAccountByIdAsync(int id);
        public Task<List<Account>> GetAccountsByNameAsync(string name);
        public Task<List<Account>> GetAccountsByCurrencyAsync(Currency currency);
        public Task<List<Account>> GetAccountsByTypeAsync(AccountType type);
        public Task<List<Account>> GetAccountsByUserIdAsync(int userId);
        public Task<Account> GetAccountByIdAndUserIdAsync(int id, int userId);
        public Task<List<Account>> GetAllAccountsWithTransactionsAsync();
        public Task<Account> GetAccountByNameAndUserIdAsync(string name, int userId);
        public Task<List<Account>> GetAccountsByTypeAndUserIdAsync(AccountType type, int userId);
        public Task<List<Account>> GetAccountsByCurrencyAndUserIdAsync(Currency currency, int userId);
        public Task<Account> GetAccountByIdWithTransactionsAsync(int id);
        public Task<List<Account>> GetAccountsByNameWithTransactionsAsync(string name);
        public Task<List<Account>> GetAccountsByTypeWithTransactionsAsync(AccountType type);
        public Task<List<Account>> GetAccountsByCurrencyWithTransactionsAsync(Currency currency);
        public Task<List<Account>> GetAccountsByUserIdWithTransactionsAsync(int userId);
        public Task<Account> GetAccountByIdAndUserIdWithTransactionsAsync(int userId, int id);
        public Task<Account> GetAccountByNameAndUserIdWithTransactionsAsync(int userId, string name);
        public Task<List<Account>> GetAccountsByTypeAndUserIdWithTransactionsAsync(int userId, AccountType type);
        public Task<List<Account>> GetAccountsByCurrencyAndUserIdWithTransactionsAsync(int userId, Currency currency);
        public Task AddAccountAsync(Account accountToAdd);
        public void UpdateAccount(Account accountToUpdate);
        public void DeleteAccount(Account accountToDelete);
        public Task AddAccountAndSaveAsync(Account accountToAdd);
        public Task UpdateAccountAndSaveAsync(Account accountToUpdate);
        public Task DeleteAccountAndSaveAsync(Account accountToDelete);
    }
}
