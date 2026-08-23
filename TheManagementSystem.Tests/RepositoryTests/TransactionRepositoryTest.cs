using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class TransactionRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfReceivingAllTransactions()
        {
            var resultCount = 3;
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "description1", TransactionType = TransactionType.Income };
            var transaction2 = new Transaction { Id = 2, Amount = 100, Date = fixedDate, Description = "description2", TransactionType = TransactionType.Income };
            var transaction3 = new Transaction { Id = 3, Amount = 100, Date = fixedDate, Description = "description3", TransactionType = TransactionType.Income };
            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3);
            await context.SaveChangesAsync();
            var result = await repository.GetAll();
            Assert.Equal(resultCount, result.Count);
        }

        [Fact]
        public async Task TheTestOfGettingAllNonExistingTransactions()
        {
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var result = await repository.GetAll();
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingTransactionById()
        {
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "description1", TransactionType = TransactionType.Income };
            var transaction2 = new Transaction { Id = 2, Amount = 100, Date = fixedDate, Description = "description2", TransactionType = TransactionType.Income };
            var transaction3 = new Transaction { Id = 3, Amount = 100, Date = fixedDate, Description = "description3", TransactionType = TransactionType.Income };
            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3);
            await context.SaveChangesAsync();
            var result = await repository.GetById(transaction1.Id);
            Assert.NotNull(result);
            Assert.Equal(transaction1.Id, result.Id);
        }

        [Fact]
        public async Task TestForReceivingATransactionsForANonExistentId()
        {
            var context = CreateContext();
            var nonExistentId = 999;
            var repository = new TransactionRepository(context);
            var result = await repository.GetById(nonExistentId);
            Assert.Null(result);
        }

        [Fact]
        public async Task TheTestOfGettingTransactionByAccountId()
        {
            var resultCount = 2;
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0); 
            var account1 = new Account { Id = 1, Name = "акаунт1", Type = AccountType.BankAccount, InitialBalance = 0, Currency = Currency.RUB };
            var account2 = new Account { Id = 2, Name = "акаунт2", Type = AccountType.CreditCard, InitialBalance = 0, Currency = Currency.RUB };
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "description1", TransactionType = TransactionType.Income, AccountId = account1.Id, Account = account1 };
            var transaction2 = new Transaction { Id = 2, Amount = 100, Date = fixedDate, Description = "description2", TransactionType = TransactionType.Income, AccountId = account1.Id, Account = account1 };
            var transaction3 = new Transaction { Id = 3, Amount = 100, Date = fixedDate, Description = "description3", TransactionType = TransactionType.Income, AccountId = account2.Id, Account = account2 };
            await context.Accounts.AddRangeAsync(account1, account2);
            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3);
            await context.SaveChangesAsync();
            var result = await repository.GetByAccountId(account1.Id);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(account1.Id, x.AccountId));
            Assert.All(result, x => Assert.Equal(account1, x.Account));
        }

        [Fact]
        public async Task TestForReceivingATransactionsForANonExistentAccountId()
        {
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var nonExistentAccountId = 999;
            var result = await repository.GetByAccountId(nonExistentAccountId);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingTransactionByCategoryId()
        {
            var resultCount = 2;
            var context = CreateContext();
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var repository = new TransactionRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            var category2 = new Category { Id = 2, Name = "name2", Type = TransactionType.Income };
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "description1", TransactionType = TransactionType.Income, CategoryId  = category1.Id, Category = category1 };
            var transaction2 = new Transaction { Id = 2, Amount = 100, Date = fixedDate, Description = "description2", TransactionType = TransactionType.Income, CategoryId = category1.Id, Category = category1 };
            var transaction3 = new Transaction { Id = 3, Amount = 100, Date = fixedDate, Description = "description3", TransactionType = TransactionType.Income, CategoryId = category2.Id, Category = category2 }; 
            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3);
            await context.Categories.AddRangeAsync(category1, category2);
            await context.SaveChangesAsync();
            var result = await repository.GetByCategoryId(category1.Id);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(category1.Id, x.CategoryId));
            Assert.All(result, x => Assert.Equal(category1, x.Category));
        }

        [Fact]
        public async Task TestForReceivingATransactionsForANonExistentCategoryId()
        {
            var context = CreateContext();
            var nonExistentCategoryId = 999;
            var repository = new TransactionRepository(context);
            var result = await repository.GetByCategoryId(nonExistentCategoryId);
            Assert.Empty(result);
        }


        [Fact]
        public async Task TheTestOfGettingTransactionByAmount()
        { 
            var amount = 100;
            var resultCount = 2;
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "description1", TransactionType = TransactionType.Income };
            var transaction2 = new Transaction { Id = 2, Amount = 100, Date = fixedDate, Description = "description2", TransactionType = TransactionType.Income };
            var transaction3 = new Transaction { Id = 3, Amount = 200, Date = fixedDate, Description = "description3", TransactionType = TransactionType.Income };
            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3);
            await context.SaveChangesAsync();
            var result = await repository.GetByAmount(amount);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(amount, x.Amount));
        }

        [Fact]
        public async Task TestForReceivingATransactionsForANonExistentAmount()
        {
            var amount = 100;
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var result = await repository.GetByAmount(amount);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingTransactionsByDate()
        {
            var resultCount = 2;
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var fixedDate2 = new DateTime(2025, 06, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "description1", TransactionType = TransactionType.Income };
            var transaction2 = new Transaction { Id = 2, Amount = 100, Date = fixedDate, Description = "description2", TransactionType = TransactionType.Income };
            var transaction3 = new Transaction { Id = 3, Amount = 100, Date = fixedDate2, Description = "description3", TransactionType = TransactionType.Income };
            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3);
            await context.SaveChangesAsync();
            var result = await repository.GetByDate(fixedDate);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(fixedDate, x.Date));
        }

        [Fact]
        public async Task TestForReceivingATransactionsForANonExistentDate()
        {
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var result = await repository.GetByDate(fixedDate);
            Assert.Empty(result);
        }


        [Fact]
        public async Task TheTestOfGettingTransactionsByType()
        {
            var resultCount = 2;
            var context = CreateContext();
            var type = TransactionType.Income;
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "description1", TransactionType = TransactionType.Income };
            var transaction2 = new Transaction { Id = 2, Amount = 100, Date = fixedDate, Description = "description2", TransactionType = TransactionType.Income };
            var transaction3 = new Transaction { Id = 3, Amount = 100, Date = fixedDate, Description = "description3", TransactionType = TransactionType.Expense };
            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3);
            await context.SaveChangesAsync();
            var result = await repository.GetByType(type);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(type, x.TransactionType));
        }

        [Fact]
        public async Task TestForReceivingATransactionsForANonExistentType()
        {
            var context = CreateContext();
            var type = TransactionType.Income;
            var repository = new TransactionRepository(context);
            var result = await repository.GetByType(type);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TransactionAdditionTest()
        {
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "descriptiion", TransactionType = TransactionType.Income };
            await repository.AddAndSave(transaction1);
            var result = await context.Transactions.FindAsync(transaction1.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TransactionUpdateTest()
        {
            var amount = 200;
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "descriptiion", TransactionType = TransactionType.Income };
            await context.Transactions.AddAsync(transaction1);
            await context.SaveChangesAsync();
            transaction1.Amount = amount;
            await repository.UpdateAndSave(transaction1);
            var result = await context.Transactions.FindAsync(transaction1.Id);
            Assert.Equal(amount, result.Amount);
        }

        [Fact]
        public async Task TransactionDeleteTest()
        {
            var context = CreateContext();
            var repository = new TransactionRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var transaction1 = new Transaction { Id = 1, Amount = 100, Date = fixedDate, Description = "descriptiion", TransactionType = TransactionType.Income };
            await context.Transactions.AddAsync(transaction1);
            await context.SaveChangesAsync();
            await repository.DeleteAndSave(transaction1);
            var result = await context.Transactions.FindAsync(transaction1.Id);
            Assert.Null(result);
        }
    }
}
