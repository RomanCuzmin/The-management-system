using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class CategoryRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfGettingAllCategories()
        {
            var resultLength = 3;
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            var category2 = new Category { Id = 2, Name = "name2", Type = TransactionType.Income };
            var category3 = new Category { Id = 3, Name = "name3", Type = TransactionType.Expense };
            await context.Categories.AddRangeAsync(category1, category2, category3);
            await context.SaveChangesAsync();
            var result = await repository.GetAll();
            Assert.Equal(resultLength, result.Count);
        }

        [Fact]
        public async Task TheTestGettingAllNonExistentCategories()
        {
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var result = await repository.GetAll();
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestGettingACategoryById()
        {
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            var category2 = new Category { Id = 2, Name = "name2", Type = TransactionType.Income };
            var category3 = new Category { Id = 3, Name = "name3", Type = TransactionType.Expense };
            await context.Categories.AddRangeAsync(category1, category2, category3);
            await context.SaveChangesAsync();
            var result = await repository.GetById(category1.Id);
            Assert.NotNull(result);
            Assert.Equal(category1.Id, result.Id);
        }

        [Fact]
        public async Task TheTestGettingACategoryByANonExistentId()
        {
            var nonExistentId = 999;
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var result = await repository.GetById(nonExistentId);
            Assert.Null(result);
        }

        [Fact]
        public async Task TheTestGettingACategoryByName()
        {
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            var category2 = new Category { Id = 2, Name = "name2", Type = TransactionType.Income };
            var category3 = new Category { Id = 3, Name = "name3", Type = TransactionType.Expense };
            await context.Categories.AddRangeAsync(category1, category2, category3);
            await context.SaveChangesAsync();
            var result = await repository.GetByName(category1.Name);
            Assert.NotNull(result);
            Assert.Equal(category1.Name, result.Name);
        }

        [Fact]
        public async Task TheTestGettingACategoryByANonExistentName()
        {
            var nonExistentName = "999";
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var result = await repository.GetByName(nonExistentName);
            Assert.Null (result);
        }

        [Fact]
        public async Task TheTestGettingACategoryByType()
        {
            var resultLength = 2;
            var type = TransactionType.Income;
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            var category2 = new Category { Id = 2, Name = "name2", Type = TransactionType.Income };
            var category3 = new Category { Id = 3, Name = "name3", Type = TransactionType.Expense };
            await context.Categories.AddRangeAsync(category1, category2, category3);
            await context.SaveChangesAsync();
            var result = await repository.GetByType(type);
            Assert.Equal(resultLength, result.Count);
            Assert.All(result, x => Assert.Equal(type, x.Type));
        }

        [Fact]
        public async Task TheTestGettingACategoryByANonExistentType()
        {
            var type = TransactionType.Transfer;
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var result = await repository.GetByType(type);
            Assert.Empty(result);
        }

        [Fact]
        public async Task CategoryAdditionTest()
        {
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            await repository.AddAndSave(category1);
            var result = await context.Categories.FindAsync(category1.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CategoryUpdateTest()
        {
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            await context.Categories.AddAsync(category1);
            await context.SaveChangesAsync();
            category1.Name = "Test";
            await repository.UpdateAndSave(category1);
            var result = await context.Categories.FindAsync(category1.Id);
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task CategoryDeleteTest()
        {
            var context = CreateContext();
            var repository = new CategoryRepository(context);
            var category1 = new Category { Id = 1, Name = "name1", Type = TransactionType.Income };
            await context.Categories.AddAsync(category1);
            await context.SaveChangesAsync();
            await repository.DeleteAndSave(category1);
            var result = await context.Categories.FindAsync(category1.Id);
            Assert.Null(result);
        }
    }
}
