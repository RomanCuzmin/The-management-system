using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;
using Xunit.Runner.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class BudgetRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfGettingAllBudgets()
        {
            using var context = CreateContext();
            var repository = new BudgetRepository(context);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var budget2 = new Budget { Id = 2, PlannedAmount = 200, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2) };
            var budget3 = new Budget {Id = 3,  PlannedAmount = 300, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) };
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetAllBudgetsWithDetailsAsync();
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task TestForReceivingAllBudgetsWithInvalidData()
        {
            using var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetAllBudgetsWithDetailsAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingABudgetById()
        {
            using var context = CreateContext();
            var repository = new BudgetRepository(context);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var budget2 = new Budget { Id = 2, PlannedAmount = 200, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2) };
            var budget3 = new Budget { Id = 3, PlannedAmount = 300, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) };
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetBudgetByIdWithDetailsAsync(budget1.Id);
            Assert.NotNull(result);
            Assert.Equal(budget1.Id, result.Id);
        }

        [Fact]
        public async Task TestForReceivingABudgetForANonExistentId()
        {
            using var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetBudgetByIdWithDetailsAsync(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task TheTestOfGettingABudgetsByUserId()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var user1 = new User { Id = 1, Email = "user1@test.com", Password = "password1", PhoneNumber = "89300697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "user2@test.com", Password = "password2", PhoneNumber = "89400798900", NotificationType = NotificationType.All };
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), UserId = user1.Id, User = user1 };
            var budget2 = new Budget { Id = 2, PlannedAmount = 200, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2), UserId = user1.Id, User = user1 };
            var budget3 = new Budget { Id = 3, PlannedAmount = 300, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), UserId = user2.Id, User = user2 };
            await context.Users.AddRangeAsync(user1, user2);
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetBudgetsByUserIdWithDetailsAsync(user1.Id);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(user1.Id, x.UserId));
            Assert.All(result, x => Assert.Equal(user1, x.User));
        }

        [Fact]
        public async Task TestForReceivingABudgetsANonExistentUserId()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetBudgetsByUserIdWithDetailsAsync(999);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingABudgetsByCategoryId()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var category1 = new Category { Id = 1, Name = "category1", Type = TransactionType.Transfer };
            var category2 = new Category { Id = 2, Name = "category2", Type = TransactionType.Transfer };
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), CategoryId = category1.Id, Category = category1 };
            var budget2 = new Budget { Id = 2, PlannedAmount = 200, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2), CategoryId = category1.Id, Category = category1 };
            var budget3 = new Budget { Id = 3, PlannedAmount = 300, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), CategoryId = category2.Id, Category = category2 };
            await context.Categories.AddRangeAsync(category1, category2);
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetBudgetsByCategoryIdWithDetailsAsync(category1.Id);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(category1.Id, x.CategoryId));
            Assert.All(result, x => Assert.Equal(category1, x.Category));
        }

        [Fact]
        public async Task TestForReceivingABudgetsANonExistentCategoryId()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetBudgetsByCategoryIdWithDetailsAsync(999);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingABudgetsByPlannedAmount()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var budget2 = new Budget { Id = 2, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2) };
            var budget3 = new Budget { Id = 3, PlannedAmount = 300, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) };
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetBudgetsByPlannedAmountWithDetailsAsync(100);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(100, x.PlannedAmount));
        }

        [Fact]
        public async Task TestForReceivingABudgetsANonExistentPlannedAmount()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetBudgetsByPlannedAmountWithDetailsAsync(999);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingABudgetsByStartDate()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var budget2 = new Budget { Id = 2, PlannedAmount = 100, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var budget3 = new Budget { Id = 3, PlannedAmount = 300, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddMonths(3) };
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetByStartDateWithDetails(DateTime.Now);
            Assert.All(result, x => Assert.Equal(DateTime.Now, x.StartDate));
        }

        [Fact]
        public async Task TestForReceivingABudgetsANonExistentStartDate()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetByStartDateWithDetails(DateTime.Now.AddMonths(5));
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingABudgetsByEndDate()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = fixedDate, EndDate = fixedDate};
            var budget2 = new Budget { Id = 2, PlannedAmount = 100, StartDate = fixedDate, EndDate = fixedDate };
            var budget3 = new Budget { Id = 3, PlannedAmount = 300, StartDate = fixedDate.AddDays(1), EndDate = fixedDate.AddMonths(3) };
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetByEndDateWithDetails(fixedDate);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(fixedDate, x.EndDate));
        }

        [Fact]
        public async Task TestForReceivingABudgetsANonExistentEndDate()
        {
            var context = CreateContext();  
            var repository = new BudgetRepository(context);
            var result = await repository.GetByEndDateWithDetails(DateTime.MinValue);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingABudgetsByDate()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = fixedDate, EndDate = fixedDate };
            var budget2 = new Budget { Id = 2, PlannedAmount = 100, StartDate = fixedDate, EndDate = fixedDate };
            var budget3 = new Budget { Id = 3, PlannedAmount = 300, StartDate = fixedDate.AddDays(1), EndDate = fixedDate.AddMonths(3) };
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3);
            await context.SaveChangesAsync();
            var result = await repository.GetByDateWithDetails(fixedDate, fixedDate);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(fixedDate, x.StartDate));
            Assert.All(result, x => Assert.Equal(fixedDate, x.EndDate));
        }


        [Fact]
        public async Task TestForReceivingABudgetsANonExistentDate()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var fixedDate = DateTime.UtcNow.AddMonths(10);
            var result = await repository.GetByDateWithDetails(fixedDate, fixedDate);
            Assert.Empty(result);
        }

        [Fact]
        public async Task BudgetAdditionTest()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = fixedDate, EndDate = fixedDate };
            await repository.AddBudgetAndSaveAsync(budget1);
            var result = await context.Budgets.FindAsync(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task BudgetUpdateTest()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = fixedDate, EndDate = fixedDate };
            await context.Budgets.AddAsync(budget1);
            await context.SaveChangesAsync();
            budget1.PlannedAmount = 200;
            await repository.UpdateBudgetAndSaveAsync(budget1);
            var result = await context.Budgets.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal(200, result.PlannedAmount);
        }

        [Fact]
        public async Task BudgetDeleteTest()
        {
            var context = CreateContext();
            var repository = new BudgetRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = fixedDate, EndDate = fixedDate };
            await context.Budgets.AddAsync(budget1);
            await context.SaveChangesAsync();
            await repository.DeleteBudgetAndSaveAsync(budget1);
            var result = await context.Budgets.FindAsync(1);
            Assert.Null(result);
        }
    }
}
