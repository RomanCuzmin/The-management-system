using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using User = TheManagementSystem.Domain.Entities.User;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class AccountRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfReceivingAllInvoices()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);

            var user = new User 
            { 
                Id = 1, 
                Email = "testuser@email.com", 
                Password = "password", NotificationType = NotificationType.All,
                PhoneNumber = "89200798900" 
            };

            var account1 = new Account 
            { 
                Id = 1, 
                Name = "акаунт1", 
                Type = AccountType.BankAccount, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                UserId = user.Id, 
                User = user
            };
            
            var account2 = new Account 
            { 
                Id = 2, 
                Name = "акаунт2", 
                Type = AccountType.CreditCard, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                UserId = user.Id,
                User = user 
            };

            var account3 = new Account 
            {
                Id = 3, 
                Name = "акаунт3", 
                Type= AccountType.BankAccount, 
                InitialBalance =0, 
                Currency = Currency.RUB, 
                UserId = user.Id, 
                User = user 
            };

            await context.Users.AddAsync(user);
            await context.AddRangeAsync( account1, account2, account3 );
            await context.SaveChangesAsync();
            var result = await repository.GetAllWithTransactions();
            Assert.Equal(3, result.Count);
            Assert.Contains(result, x => x.Id == account1.Id);
            Assert.Contains(result, x => x.Id == account2.Id);
            Assert.Contains(result, x => x.Id == account3.Id);
            Assert.All(result, x => Assert.Equal(user, x.User));
            Assert.All(result, x => Assert.Equal(user.Id, x.UserId));
        }

        [Fact]
        public async Task TestGettingAllInvoicesWithInvalidData()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);

            var user = new User
            {
                Id = 1,
                Email = "testuser@email.com",
                Password = "password",
                NotificationType = NotificationType.All,
                PhoneNumber = "89200798900"
            };

            var result = await repository.GetAllWithTransactions();
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingAnInvoiceById()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);

            var user = new User 
            {
                Id = 1, 
                Email = "testuser@email.com", 
                Password = "password", 
                NotificationType = NotificationType.All, 
                PhoneNumber = "89200798900" 
            };
           
            var account1 = new Account 
            {
                Id = 1, 
                Name = "акаунт1",
                Type = AccountType.BankAccount, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                UserId = user.Id,
                User = user 
            };

            var account2 = new Account 
            {
                Id = 2,
                Name = "акаунт2", 
                Type = AccountType.CreditCard, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                UserId = user.Id, 
                User = user 
            };

            var account3 = new Account
            { 
                Id = 3, 
                Name = "акаунт3", 
                Type = AccountType.BankAccount, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                UserId = user.Id, 
                User = user 
            };

            await context.Users.AddAsync(user);
            await context.Accounts.AddRangeAsync(account1, account2, account3);
            await context.SaveChangesAsync();
            var result = await repository.GetByIdWithTransactions(account1.Id);
            Assert.NotNull(result);
            Assert.Equal(account1.Id, result.Id);
        }

        [Fact]
        public async Task TheTestOfGettingAnInvoiceWithANonExistentId()
        {
            var id = 999;
            using var context = CreateContext();
            var repository = new AccountRepository(context);

            var user = new User
            {
                Id = 1,
                Email = "testuser@email.com",
                Password = "password",
                NotificationType = NotificationType.All,
                PhoneNumber = "89200798900"
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            var result  = await repository.GetByIdWithTransactions(id);
            Assert.Null(result);
        }

        [Fact]
        public async Task TheTestOfGettingAnInvoiceByUserId()
        {
            var resultCount = 3;
            using var context = CreateContext();
            var repository = new AccountRepository(context);

            var user = new User 
            {
                Id = 1, 
                Email = "testuser@email.com", 
                Password = "password", 
                NotificationType = NotificationType.All, 
                PhoneNumber = "89200798900" 
            };

            var account1 = new Account
            { 
                Id = 1, 
                Name = "акаунт1", 
                Type = AccountType.BankAccount, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                User = user, 
                UserId = user.Id 
            };

            var account2 = new Account 
            {
                Id = 2, 
                Name = "акаунт2", 
                Type = AccountType.CreditCard, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                User = user, 
                UserId = user.Id 
            };

            var account3 = new Account 
            {
                Id = 3,
                Name = "акаунт3", 
                Type = AccountType.BankAccount, 
                InitialBalance = 0, 
                Currency = Currency.RUB, 
                User = user, 
                UserId = user.Id 
            };
            
            await context.Users.AddAsync(user);
            await context.Accounts.AddRangeAsync(account1, account2, account3);
            await context.SaveChangesAsync();
            var result = await repository.GetByUserIdWitnTransactions(user.Id);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(user.Id, x.UserId));
            Assert.All(result, x => Assert.Equal(user, x.User));
            Assert.Contains(result, x => x.Id == account1.Id);
            Assert.Contains(result, x => x.Id == account2.Id);
        }

        [Fact]
        public async Task TestOfGettingAnAccountWithANonExistentUserId()
        {
            var userId = 999;
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var result = await repository.GetByUserIdWitnTransactions(userId);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingAnAccountByName()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var user1 = new User { Id = 1, Email = "testuser@email.com", Password = "password", NotificationType = NotificationType.All, PhoneNumber = "89200798900" };
            var user2 = new User { Id = 2, Email = "testuser2@email.cow", Password = "password2", NotificationType = NotificationType.ByPhoneNumber, PhoneNumber = "89200996900" };
            var account1 = new Account { Id = 1, Name = "акаунт1", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.RUB, UserId = user1.Id, User = user1 };
            var account2 = new Account { Id = 2, Name = "акаунт2", Type = AccountType.CreditCard, InitialBalance = 0, Currency = Currency.RUB, User = user1, UserId = user1.Id };
            var account3 = new Account { Id = 3, Name = "акаунт3", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.RUB, User = user2, UserId = user2.Id };
            await context.Accounts.AddRangeAsync(account1, account2, account3);
            await context.Users.AddRangeAsync( user1, user2 );
            await context.SaveChangesAsync();
            var result = await repository.GetByNameAndUserIdWithTransactions(user1.Id, account1.Name);
            Assert.NotNull(result);
            Assert.Equal(result.Name, account1.Name);
        }

        [Fact]
        public async Task TheTestOfGettingAnAccountWithANonExistentUserName()
        {
            var name = "999";
            var userId = 999;
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var result = await repository.GetByNameAndUserIdWithTransactions(userId, name);
            Assert.Null(result);
        }

        [Fact]
        public async Task TheTestOfGettingAnAccountByCurrency()
        {
            var currency = Currency.RUB;
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var user1 = new User { Id = 1, Email = "testuser@email.com", Password = "password", NotificationType = NotificationType.All, PhoneNumber = "89200798900" };
            var user2 = new User { Id = 2, Email = "testuser2@email.cow", Password = "password2", NotificationType = NotificationType.ByPhoneNumber, PhoneNumber = "89200996900" };
            var account1 = new Account { Id = 1, Name = "акаунт1", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.RUB, User = user1, UserId = user1.Id };
            var account2 = new Account { Id = 2, Name = "акаунт2", Type = AccountType.CreditCard, InitialBalance = 0, Currency = Currency.RUB, User = user1, UserId = user1.Id };
            var account3 = new Account { Id = 3, UserId = user2.Id, Name = "акаунт3", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.EUR, User = user2 };
            await context.Accounts.AddRangeAsync( account1, account2, account3 );
            await context.SaveChangesAsync();
            var result = await repository.GetByCurrencyAndUserIdWithTransactions(user1.Id, currency);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(Currency.RUB, x.Currency));
        }

        [Fact]
        public async Task TheTestOfGettingAnInvoiceForANonExistentCurrency()
        {
            var userId = 999;
            var currency = Currency.BYN;
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var result = await repository.GetByCurrencyAndUserIdWithTransactions(userId, currency);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingAnAccountByType()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var user1 = new User { Id = 1, Email = "testuser@email.com", Password = "password", NotificationType = NotificationType.All, PhoneNumber = "89200798900" };
            var user2 = new User { Id = 2, Email = "testuser2@email.cow", Password = "password2", NotificationType = NotificationType.ByPhoneNumber, PhoneNumber = "89200996900" };
            var account1 = new Account { Id = 1, Name = "акаунт1", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.RUB, User = user1, UserId = user1.Id };
            var account2 = new Account { Id = 2, Name = "акаунт2", Type = AccountType.CreditCard, InitialBalance = 0, Currency = Currency.RUB, User = user1, UserId = user1.Id  };
            var account3 = new Account { Id = 3, Name = "акаунт3", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.EUR, User = user1, UserId = user1.Id };
            await context.Accounts.AddRangeAsync(account1, account2, account3);
            await context.Users.AddRangeAsync( user1, user2 );
            await context.SaveChangesAsync();
            var result = await repository.GetByTypeAndUserIdWithTransactions(user1.Id,AccountType.BankAccount);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(AccountType.BankAccount, x.Type));
        }

        [Fact]
        public async Task AccountAdditionTest()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var account = new Account { Id = 1, Name = "акаунт", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.EUR };
            await repository.AddAndSave(account);
            var result = await context.Accounts.FindAsync(account.Id);
            await context.SaveChangesAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AccountUpdateTest()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var account = new Account { Id = 1,  Name = "акаунт", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.EUR};
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            account.Name = "обновленныйакаунт";
            await repository.UpdateAndSave(account);
            await context.SaveChangesAsync();
            var result = await context.Accounts.FindAsync();
            Assert.Equal("обновленныйакаунт", result.Name);
        }

        [Fact]
        public async Task AccountDeleteTest()
        {
            using var context = CreateContext();
            var repository = new AccountRepository(context);
            var account = new Account { Id = 1, Name = "акаунт", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.EUR };
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            await repository.DeleteAndSave(account);
            await context.SaveChangesAsync();
            var result = await context.Accounts.FindAsync(account.Id);
            Assert.Null(result);
        }
    }
}