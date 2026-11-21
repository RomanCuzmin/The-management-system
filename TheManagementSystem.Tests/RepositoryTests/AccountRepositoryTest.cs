using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Net.WebSockets;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;
using Xunit.Runner.Common;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using User = TheManagementSystem.Domain.Entities.User;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class AccountRepositoryTest
    {
        // Метод для создания ApplicationDbContext
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        // Асинхронный метод для добавления данных
        private async Task SeedData(ApplicationDbContext context)
        {
            var date = new DateOnly(2025, 4, 17);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password= "password", PhoneNumber = "8920069697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "user2@email.com", Password = "password2", PhoneNumber = "892300797900", NotificationType = NotificationType.All };
            
            var transactions1 = new List<Transaction>
            {
                new Transaction { Id = 1, Amount = 100, Date = date, Description = "description", TransactionType = TransactionType.Transfer, UserId = user1.Id, User = user1, },
                new Transaction { Id = 2, Amount = 100, Date = date, Description = "description", TransactionType = TransactionType.Transfer, UserId = user1.Id, User = user1 },
                new Transaction { Id = 3, Amount = 100, Date = date, Description = "description", TransactionType = TransactionType.Transfer, UserId = user1.Id, User = user1 }
            };

            var transactions2 = new List<Transaction>
            {
                new Transaction { Id = 4, Amount = 100, Date = date, Description = "description", TransactionType = TransactionType.Transfer, UserId = user2.Id, User = user2, },
                new Transaction { Id = 5, Amount = 100, Date = date, Description = "description", TransactionType = TransactionType.Transfer, UserId = user2.Id, User = user2 },
                new Transaction { Id = 6, Amount = 100, Date = date, Description = "description", TransactionType = TransactionType.Transfer, UserId = user2.Id, User = user2 }
            };

            var account1 = new Account { Id = 1, Name = "account1", InitialBalance = 0, Currency = Currency.RUB, Type = AccountType.BankAccount, Transactions = transactions1, UserId = user1.Id, User = user1 };
            var account2 = new Account { Id = 2, Name = "account2", InitialBalance = 0, Currency = Currency.RUB, Type = AccountType.BankAccount, Transactions = transactions1, UserId = user1.Id, User = user1 };
            var account3 = new Account { Id = 3, Name = "account3", InitialBalance = 0, Currency = Currency.USD, Type = AccountType.Cash, Transactions = transactions2, UserId = user2.Id, User = user2 };

            await context.Users.AddRangeAsync(user1, user2);
            await context.Transactions.AddRangeAsync(transactions1);
            await context.Transactions.AddRangeAsync(transactions2);
            await context.Accounts.AddRangeAsync(account1, account2, account3);
            await context.SaveChangesAsync();
        }

        #region Тесты методов для получения счетов
        // Тест асинхронного метода для получения всех счетов с существующими данными
        [Fact]
        public async Task GetAllAccountsAsync_ReturnsAllAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository =  new AccountRepository(context);
            var result = await repository.GetAllAccountsAsync();
            var resultCount = 3;
            Assert.Equal(resultCount, result.Count);
        }

        // Тест асинхронного метода для получения всех счетов с несуществующими данными
        [Fact]
        public async Task GetAllAccountsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var result = await repository.GetAllAccountsAsync();
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счета по id с существующими данными
        [Fact]
        public async Task GetAccountByIdAsync_ExistingId_ReturnsAccount()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var id = 1;
            var result = await repository.GetAccountByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        // Тест асинхронного метода для получения счета по id с несуществующими данными
        [Fact]
        public async Task GetAccountById_NonExistingId_ReturnsNull()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var id = 999;
            var result = await repository.GetAccountByIdAsync(id);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получение счетов по именни с существующими данными
        [Fact]
        public async Task GetAccountsByNameAsync_ReturnsMatchingAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "account1";
            var result = await repository.GetAccountsByNameAsync(name);
            var resultCount = 1;
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(name, x.Name));
        }

        // Тест асинхронного метода для получения счетов по именни с несуществующими данными
        [Fact]
        public async Task GetAccountsByNameAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "999";
            var result = await repository.GetAccountsByNameAsync(name);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счетов по типу с существующими данными
        [Fact]
        public async Task GetAccountsByTypeAsync_ReturnsMatchingAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var type = AccountType.BankAccount;
            var result = await repository.GetAccountsByTypeAsync(type);
            var resultCount = 2;
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(type, x.Type));
        }

        // Тест асинхронного метода для получения счетов по типу с несуществующими данными
        [Fact]
        public async Task GetAccountsByTypeAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var type = AccountType.CreditCard;
            var result = await repository.GetAccountsByTypeAsync(type);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счетов по валюте с существующими даннымим
        [Fact]
        public async Task GetAccountsByCurrencyAsync_ReturnsMatchingAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var currency = Currency.RUB;
            var result = await repository.GetAccountsByCurrencyAsync(currency);
            var resultCount = 2;
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(currency, x.Currency));
        }

        // Тест асинхронного метода для получения счетов по валюте с несуществующими данными
        [Fact]
        public async Task GetAccountsByCurrencyAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var currency = Currency.CNY;
            var result = await repository.GetAccountsByCurrencyAsync(currency);
            Assert.Empty(result);
        }
        #endregion
        #region Тесты методов для получения счетов пользователя
        // Тест асинхронного метода для получения всех счетов пользователя с существуещими данными 
        [Fact]
        public async Task GetAccountsByUserIdAsync_ReturnsUserAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 1;
            var result = await repository.GetAccountsByUserIdAsync(userId);
            var resultCount = 2;
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхронного метода для получения всех счетов пользователя с несуществующими данными
        [Fact]
        public async Task GetAccountsByUserIdAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 999;
            var result = await repository.GetAccountsByUserIdAsync(userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счета пользователя по id с существующими данными 
        [Fact]
        public async Task GetAccountByIdAndUserIdAsync_ReturnsAccount()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var id = 1;
            var userId = 1;
            var result = await repository.GetAccountByIdAndUserIdAsync(id, userId);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(userId, result.UserId);
        }

        // Тест асинхронного метода для получения счета пользователя по id с несуществующими данными
        [Fact]
        public async Task GetAccountByIdAndUserIdAsync_ReturnsNull()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var id = 999;
            var userId = 999;
            var result = await repository.GetAccountByIdAndUserIdAsync(id, userId);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения счета пользователя по именни с существующими данными 
        [Fact]
        public async Task GetAccountByNameAndUserIdAsync_ReturnsAccount()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "account1";
            var userId = 1;
            var result = await repository.GetAccountByNameAndUserIdAsync(name, userId);
            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(userId, result.UserId);
        }

        // Тест асинхронного метода для получения счета пользователя по именни с несуществующими данными
        [Fact]
        public async Task GetAccountByNameAndUserIdAsync_ReturnsNull()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "999";
            var userId = 999;
            var result = await repository.GetAccountByNameAndUserIdAsync(name, userId);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения счета пользователя по типу с существующими данными
        [Fact]
        public async Task GetAccountByTypeAndUserIdAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 1;
            var type = AccountType.BankAccount;
            var resultCount = 2;
            var result = await repository.GetAccountsByTypeAndUserIdAsync(type, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(type, x.Type));
        }

        // Тест асинхронного метода для получения счета пользователя по типу с несуществующими данными 
        [Fact]
        public async Task GetAccountByTypeAndUserIdAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 999;
            var type = AccountType.Deposit;
            var result = await repository.GetAccountsByTypeAndUserIdAsync(type, userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счета пользователя по валлюте с существующими данными
        [Fact]
        public async Task GetAccountsByCurrencyAndUserIdAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 1;
            var currency = Currency.RUB;
            var resultCount = 2;
            var result = await repository.GetAccountsByCurrencyAndUserIdAsync(currency, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(currency, x.Currency));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхронного метода для получения счета пользователя по валюте с несуществующими данными
        [Fact]
        public async Task GetAccountsByCurrencyAndUserIdAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 999;
            var currency = Currency.BYN;
            var result = await repository.GetAccountsByCurrencyAndUserIdAsync(currency, userId);
            Assert.Empty(result);
        }
        #endregion
        #region Тесты методов для получения счетов c загруженными транзакциями
        // Тест асинхронного метода для получения всех счетов с загруженными данными с существующими данными
        [Fact]
        public async Task GetAllAccountsWithTransactionsAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var result = await repository.GetAllAccountsWithTransactionsAsync();
            var resultCount = 3;
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.NotEmpty(x.Transactions));
        }

        // Тест асинхронного метода для получения всех счетов с загруженными данными с несуществующими данными
        [Fact]
        public async Task GetAllAccountsWithTransactionsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var result = await repository.GetAllAccountsWithTransactionsAsync();
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счета по id с загруженными данными с существующими данными
        [Fact]
        public async Task GetAccountByIdWithTransactionsAsync_ReturnsAccount()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var id = 1;
            var result = await repository.GetAccountByIdWithTransactionsAsync(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.NotNull(result.Transactions);
        }

        // Тест асинхронного метода для получения счета по id с загруженными данными с невалидными данными
        [Fact]
        public async Task GetAccountByIdWithTransactionsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var id = 999;
            var result = await repository.GetAccountByIdWithTransactionsAsync(id);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получкения счета по именни с загруженными данными с валидными данными
        [Fact]
        public async Task GetAccountByNameWithTransactionsAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "account1";
            var rexsultCount = 1;
            var result = await repository.GetAccountsByNameWithTransactionsAsync(name);
            Assert.NotEmpty(result);
            Assert.Equal(rexsultCount, result.Count);
            Assert.All(result, x => Assert.Equal(name, x.Name));
            Assert.All(result, x => Assert.NotEmpty(x.Transactions));
        }

        // Тест асинхронного метода для получения счета по именни с загруженными данными с невалидными данными
        [Fact]
        public async Task GetAccountByNameWithTransactionsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "999";
            var result = await repository.GetAccountsByNameWithTransactionsAsync(name);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счетов по типу с загруженными данными с валидными даннымми
        [Fact]
        public async Task GetAccountByTypeWithTransactionsAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var type = AccountType.BankAccount;
            var resultCount = 2;
            var result = await repository.GetAccountsByTypeWithTransactionsAsync(type);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(type, x.Type));
            Assert.All(result, x => Assert.NotEmpty(x.Transactions));
        }

        // Тест асинхронного метода для получения счетов по типу с загруженными данными с невалидными данными
        [Fact]
        public async Task GetAccountByTypeWithTransactionsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var type = AccountType.Investment;
            var result = await repository.GetAccountsByTypeWithTransactionsAsync(type);
            Assert.Empty(result);
        }
        #endregion
        #region Тесты методов для получения счетов пользователя c загруженными транзакциями
        // Тест асинхронного метода для получения всех счетов пользователя с загруженными транзакциями с валидными данными
        [Fact]
        public async Task GetAllUserAccountsWithTransactionsAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetUserAccountsWithTransactionsAsync(userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
            Assert.All(result, x => Assert.NotEmpty(x.Transactions));
        }

        // Тест асинхронного метода для получения всех счетов пользователя с загруженными транззакциями с невалидными данными
        [Fact]
        public async Task GetAllUsersAccountsWithTransactionAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var userId = 999;
            var result = await repository.GetUserAccountsWithTransactionsAsync(userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счета пользователя по id с загруженными транзакциями с валидными данными
        [Fact]
        public async Task GetUserAccountByIdWithTransactionsAsync_ExistingId_ReturnsAccount()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var accountId = 1;
            var userId = 1;
            var result = await repository.GetUserAccountByIdWithTransactionsAsync(accountId, userId);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Transactions);
            Assert.Equal(accountId, result.Id);
            Assert.Equal(userId, result.UserId);
        }

        // Тест асинхронного метода для получения счета пользователя по id с загруженными транзакциями с невалидными данными
        [Fact]
        public async Task GetUserAccountByIdWithTransactionAsync_NonExistingId_ReturnNull()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var accountId = 999;
            var userId = 999;
            var result = await repository.GetUserAccountByIdWithTransactionsAsync(accountId, userId);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения счетов пользователя по имени с загруженными транзакциями с валидными данными
        [Fact]
        public async Task GetUserAccountsByNameWithTransactionsAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "account1";
            var userId = 1;
            var result = await repository.GetUserAccountByNameWithTransactionsAsync(userId, name);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Transactions);
            Assert.Equal(name, result.Name);
            Assert.Equal(userId, result.UserId);
        }

        // Тест асинхронного метода для получения счетов пользователя по имени с загруженными транзакциями с невалидными данными
        [Fact]
        public async Task GetUserAccountsByNameWithTransactionsAsync_ReturnsNull()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var name = "999";
            var userId = 999;
            var result = await repository.GetUserAccountByNameWithTransactionsAsync(userId, name);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения счетов пользователя по типу с загруженными транзакциями с валидными данными
        [Fact]
        public async Task GetUserAccountsByTypeWithTransactionsAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 1;
            var type = AccountType.BankAccount;
            var resultCount = 2;
            var result = await repository.GetUserAccountsByTypeWithTransactionsAsync(userId, type);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.NotEmpty(x.Transactions));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
            Assert.All(result, x => Assert.Equal(type, x.Type));
        }

        // Тест асинхронного метода для получения счетов пользователя по типу с загруженными транзакциями с невалидными данными
        [Fact]
        public async Task GetUserAccountsByTypeWithTransactionsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 999;
            var type = AccountType.Deposit;
            var result = await repository.GetUserAccountsByTypeWithTransactionsAsync(userId, type);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения счетов пользователя по валюте с загруженными транзакциями с валидными данными
        [Fact]
        public async Task GetUserAccountsByCurrencyWithTransactionsAsync_ReturnsAccounts()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 1;
            var currency = Currency.RUB;
            var resultCount = 2;
            var result = await repository.GetUserAccountsByCurrencyWithTransactionsAsync(userId, currency);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.NotEmpty(x.Transactions));
            Assert.All(result, x => Assert.Equal(x.UserId, userId));
            Assert.All(result, x => Assert.Equal(x.Currency, currency));
        }

        // Тест асинхронного метода для получения счетов пользователя по валюте с загруженными транзакциями с невалидными данными
        [Fact]
        public async Task GetUserAccountsByCurrencyWithTransactionsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var userId = 999;
            var currency = Currency.BYN;
            var result = await repository.GetUserAccountsByCurrencyWithTransactionsAsync(userId, currency);
            Assert.Empty(result);
        }
        #endregion
        #region Тест методов для добавления, обновления или удаления с сохранением или без
        // Тест асинхронного метода для добавления счета без сохранения
        [Fact]
        public async Task AddAccountAsync_AddsAccountToContextWithoutSaving()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var newAccount = new Account { Id = 4, Name = "account3", InitialBalance = 0, Currency = Currency.USD, Type = AccountType.Cash };
            await repository.AddAccountAsync(newAccount);
            var tracked = context.ChangeTracker.Entries<Account>().FirstOrDefault(x => x.Entity.Id == newAccount.Id);
            var check = await context.Accounts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == newAccount.Id);
            Assert.NotNull(tracked);
            Assert.Equal(EntityState.Added, tracked.State);
            Assert.Null(check);
        }

        // Тест метода для обновления счета без сохранения
        [Fact]
        public async Task UpdateAccount_UpdatesAccountInContextWithoutSaving()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var accountId = 1;
            var accountToUpdate = await context.Accounts.FindAsync(accountId);
            accountToUpdate.Name = "updateName";
            repository.UpdateAccount(accountToUpdate);
            var tracked = context.ChangeTracker.Entries<Account>().FirstOrDefault(x => x.Entity.Id == accountToUpdate.Id);
            var check = await context.Accounts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == accountId);
            Assert.NotNull(tracked);
            Assert.Equal(EntityState.Modified, tracked.State);
            Assert.Equal("account1", check.Name);
        }

        // Тест метода для удаления счета без сохранения
        [Fact]  
        public async Task DeleteAccountAndSaveAsync_DeletesAccountWithoutSaves()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var accountId = 1;
            var accountToDelete = await context.Accounts.FindAsync(accountId);
            repository.DeleteAccount(accountToDelete);
            var tracked = context.ChangeTracker.Entries<Account>().FirstOrDefault(x => x.Entity.Id == accountToDelete.Id);
            Assert.NotNull(tracked);
            Assert.Equal(EntityState.Deleted, tracked.State);
            Assert.NotNull(accountToDelete);
        }
        
        // Тест асинхронного метода для добавления счета с сохранением
        [Fact]
        public async Task AddAccountAndSaveAsync_AddsAccountAndSaves()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var newAccount = new Account { Id = 4, Name = "account3", InitialBalance = 0, Currency = Currency.USD, Type = AccountType.Cash };
            await repository.AddAccountAndSaveAsync(newAccount);
            var result = await context.Accounts.SingleOrDefaultAsync(x => x.Id == newAccount.Id);
            Assert.NotNull(result);
        }

        // Тест асинхронного метода для обновления счета с сохранением 
        [Fact]
        public async Task UpdateAccountAndSaveAsync_UpdatesAccountAndSaves()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var accountId = 1;
            var newName = "updateName";
            var accountToUpdate = await context.Accounts.FindAsync(accountId);
            accountToUpdate.Name = newName;
            await repository.UpdateAccountAndSaveAsync(accountToUpdate);
            var result = await context.Accounts.SingleOrDefaultAsync(x => x.Id == accountId);
            Assert.Equal(newName, accountToUpdate.Name);
        }

        // Тест асинхронного метода для удаления счета с сохранением
        [Fact]
        public async Task DeleteAccountAndSaveAsync_DeletesAccountAndSaves()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new AccountRepository(context);
            var accountId = 1;
            var accountToDelete = await context.Accounts.FindAsync(accountId);
            await repository.DeleteAccountAndSaveAsync(accountToDelete);
            var result = await context.Accounts.SingleOrDefaultAsync(x => x.Id == accountId);
            Assert.Null(result);
        }
        #endregion

    }
}