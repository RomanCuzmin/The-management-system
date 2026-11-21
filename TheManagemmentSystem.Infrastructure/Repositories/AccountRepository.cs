using Microsoft.EntityFrameworkCore;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор репозитория AccountRepository
        public AccountRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        #region Методы для получения счетов 
        // Асинхронный метод для получения всех счетов из базы данных
        public async Task<List<Account>> GetAllAccountsAsync() => await _context.Accounts.ToListAsync();
        // Асинхронный метод для получения счета по id из базы данных
        public async Task<Account?> GetAccountByIdAsync(int id) => await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения счетов по именни из базы данных 
        public async Task<List<Account>> GetAccountsByNameAsync(string name) => await _context.Accounts.Where(x => x.Name == name).ToListAsync();
        // Асинхронный метод для получения счетов по типу из базы данных 
        public async Task<List<Account>> GetAccountsByTypeAsync(AccountType type) => await _context.Accounts.Where(x =>x.Type == type).ToListAsync();
        // Асинхронный метод для получения счетов по валюте из базы данных
        public async Task<List<Account>> GetAccountsByCurrencyAsync(Currency currency) => await _context.Accounts.Where(x => x.Currency == currency).ToListAsync();
        #endregion
        #region Методы для получения счетов пользователя    
        // Асинхронный метод для получения всех счетов пользователя из базы данных      
        public async Task<List<Account>> GetAccountsByUserIdAsync(int userId) => await _context.Accounts.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения счета пользователя по id  из базы данных                                                                                                                                         
        public async Task<Account?> GetAccountByIdAndUserIdAsync(int id, int userId) => await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения счета пользователя по имени из базы данных 
        public async Task<Account?> GetAccountByNameAndUserIdAsync(string name, int userId) => await _context.Accounts.SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        // Асинхронный метод для получения счетов пользователя по типу из базы данных
        public async Task<List<Account>> GetAccountsByTypeAndUserIdAsync(AccountType type, int userId) => await _context.Accounts.Where(x => x.Type == type && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения счетов пользователя по валюте из базы данных
        public async Task<List<Account>> GetAccountsByCurrencyAndUserIdAsync(Currency currency, int userId) => await _context.Accounts.Where(x => x.Currency == currency && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для получения счетов с транзакциями 
        // Асинхронный метод для получения всех счетов с транзакциями из базы данных
        public async Task<List<Account>> GetAllAccountsWithTransactionsAsync() => await _context.Accounts.Include(x => x.Transactions).ToListAsync();
        // Асинхронный метод для получения счета с транзакциями по id из базы данных
        public async Task<Account?> GetAccountByIdWithTransactionsAsync(int id) => await _context.Accounts.Include(x => x.Transactions).SingleOrDefaultAsync(x =>  x.Id == id);
        // Асинхронный метод для получения счетов по имени с транзакциями из базы данных
        public async Task<List<Account>> GetAccountsByNameWithTransactionsAsync(string name) => await _context.Accounts.Include(x => x.Transactions).Where(x => x.Name == name).ToListAsync();
        // Асинхронный метод для получения счетов по типу с транзакциями из базы данных
        public Task<List<Account>> GetAccountsByTypeWithTransactionsAsync(AccountType type) => _context.Accounts.Include(x => x.Transactions).Where(x => x.Type == type).ToListAsync();
        // Асинхронный метод для получения счетов по валюте с транзакциями из базы данных
        public async Task<List<Account>> GetAccountsByCurrencyWithTransactionsAsync(Currency currency) => await _context.Accounts.Include(x => x.Transactions).Where(x => x.Currency == currency).ToListAsync();
        #endregion
        #region Методы для получения счетов пользователя с транзакциями 
        // Асинхронный метод для получения счетов пользователя с транзакциями из  базы данных 
        public async Task<List<Account>> GetUserAccountsWithTransactionsAsync(int userId) => await _context.Accounts.Include(x => x.Transactions).Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения счета пользователя по id с транзакциями из базы данных
        public async Task<Account?> GetUserAccountByIdWithTransactionsAsync(int userId, int id) => await _context.Accounts.Include(x => x.Transactions).SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);
        // Асинхронный метод для получения счета пользователя по именни  с транзакциями 
        public async Task<Account?> GetUserAccountByNameWithTransactionsAsync(int userId, string name) => await _context.Accounts.Include(x => x.Transactions).SingleOrDefaultAsync(x => x.UserId == userId && x.Name == name);
        // Асинхронный метод для получения счетов пользователя по типу с транзакциями 
        public async Task<List<Account>> GetUserAccountsByTypeWithTransactionsAsync(int userId, AccountType type) => await _context.Accounts.Include(x => x.Transactions).Where(x => x.UserId == userId && x.Type == type).ToListAsync();
        // Асинхронный метод для получения счетов пользователя по валюте с транзакциями
        public async Task<List<Account>> GetUserAccountsByCurrencyWithTransactionsAsync(int userId, Currency currency) => await _context.Accounts.Include(x => x.Transactions).Where(x => x.UserId == userId && x.Currency == currency).ToListAsync();
        #endregion
        #region Методы  для добавления, обновления, удаления акаунта с сохранением или без
        // Асинхронный метод для добавления счета в базу данных без сохранения
        public async Task AddAccountAsync(Account accountToAdd) => await _context.Accounts.AddAsync(accountToAdd);
        // Метод для обновления счета в базе данных без сохранения
        public void UpdateAccount(Account accountToUpdate) => _context.Accounts.Update(accountToUpdate);
        // Метод для удаления счета из базы данных без сохранения
        public void DeleteAccount(Account accountToDelete) => _context.Accounts.Remove(accountToDelete);

        // Асинхронный метод для добавления счета в базу данных с сохранением
        public async Task AddAccountAndSaveAsync(Account accountToAdd)
        {
            await _context.AddAsync(accountToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления счета в базе данных без сохранения
        public async Task UpdateAccountAndSaveAsync(Account accountToUpdate)
        {
            _context.Accounts.Update(accountToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления счета в базе данных без сохранения
        public async Task DeleteAccountAndSaveAsync(Account accountToDelete)
        {
            _context.Remove(accountToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
