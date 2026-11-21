using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using Xunit.Runner.Common;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class BudgetRepositoryTest
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
            var startDate1 = new DateOnly(2025, 4, 17);
            var endDate1 = new DateOnly(2025, 5, 17);
            var startDate2 = new DateOnly(2025, 6, 17);
            var endDate2 = new DateOnly(2025, 6, 17);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password", PhoneNumber = "8920069697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "user2@email.com", Password = "password2", PhoneNumber = "892300797900", NotificationType = NotificationType.All };
            var category1 = new Category { Id = 1, Name = "user1category1", Type = TransactionType.Income, UserId = user1.Id, User = user1 };
            var category2 = new Category { Id = 2, Name = "user1category2", Type = TransactionType.Transfer, UserId = user1.Id, User = user1 };
            var category3 = new Category { Id = 3, Name = "user2category1", Type = TransactionType.Income, UserId= user2.Id, User = user2 };
            var category4 = new Category { Id = 4, Name = "user2category2", Type = TransactionType.Transfer, UserId = user2.Id, User = user2 };
            var budget1 = new Budget { Id = 1, PlannedAmount = 100, StartDate = startDate1, EndDate = endDate1, UserId = user1.Id, User = user1, CategoryId = category1.Id, Category = category1 };
            var budget2 = new Budget { Id = 2, PlannedAmount = 100, StartDate = startDate1, EndDate = endDate1, UserId = user1.Id, User = user1, CategoryId = category1.Id, Category = category1 };
            var budget3 = new Budget { Id = 3, PlannedAmount = 200, StartDate = startDate2, EndDate = endDate2, UserId = user1.Id, User = user1, CategoryId = category2.Id, Category = category2 };
            var budget4 = new Budget { Id = 4, PlannedAmount = 200, StartDate = startDate2, EndDate = endDate2, UserId = user1.Id, User = user1, CategoryId = category2.Id, Category = category2 };
            var budget5 = new Budget { Id = 5, PlannedAmount = 100, StartDate = startDate1, EndDate = endDate1, UserId = user2.Id, User = user2, CategoryId = category3.Id, Category = category3 };
            var budget6 = new Budget { Id = 6, PlannedAmount = 100, StartDate = startDate1, EndDate = endDate1, UserId = user2.Id, User = user2, CategoryId = category3.Id, Category = category3 };
            var budget7 = new Budget { Id = 7, PlannedAmount = 200, StartDate = startDate2, EndDate = endDate2, UserId = user2.Id, User = user2, CategoryId = category4.Id, Category = category4 };
            var budget8 = new Budget { Id = 8, PlannedAmount = 200, StartDate = startDate2, EndDate = endDate2, UserId = user2.Id, User = user2, CategoryId = category4.Id, Category = category4 };
            await context.Users.AddRangeAsync(user1, user2);
            await context.Categories.AddRangeAsync(category1, category2, category3, category4);
            await context.Budgets.AddRangeAsync(budget1, budget2, budget3, budget4, budget5, budget6, budget7, budget8);
            await context.SaveChangesAsync();
        }

        #region Тесты методов для получения бюджетов
        // Тест асинхронного метода для получения всех бюджетов с валидными данными
        [Fact]
        public async Task GetAllBudgetsAsync_ReturnsAllBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var resultLength = 8;
            var result = await repository.GetAllBudgetsAsync();
            Assert.Equal(resultLength, result.Count);
        }

        // Тест асинхронного метода для получения всех бюджетов с невалидными данными
        [Fact]
        public async Task GetAllBudgetsAsync_ReturnsEmpty()
        {
            using var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetAllBudgetsAsync();
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджета по id с валидными данными
        [Fact]
        public async Task GetBudgetById_ReturnsBudget()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var id = 1;
            var result = await repository.GetBudgetByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        // Тест асинхронного метода для получения бюджета по id с невалидными данными
        [Fact]
        public async Task GetBudgetById_ReturnsNull()
        {
            using var context = CreateContext();
            var repository = new BudgetRepository(context);
            var id = 999;
            var result = await repository.GetBudgetByIdAsync(id);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения бюджета по id категории с валидными данными
        [Fact]
        public async Task GetBudgetsByIdCategory_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 1;
            var resultLength = 2;
            var result = await repository.GetBudgetsByCategoryIdAsync(categoryId);
            Assert.NotEmpty(result);
            Assert.Equal(resultLength, result.Count());
            Assert.All(result, x => Assert.Equal(categoryId, x.CategoryId));
        }

        // Тест асинхронного метода для получения бюджета по id категории с невалидными данными
        [Fact]
        public async Task GetBudgetsByIdCategory_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 999;
            var result = await repository.GetBudgetsByCategoryIdAsync(categoryId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджета по запланированной сумме с валидными данными
        [Fact]
        public async Task GetBudgetsByPlannedAmount_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 100;
            var resultLength = 4;
            var result = await repository.GetBudgetsByPlannedAmountAsync(plannedAmount);
            Assert.NotEmpty(result);
            Assert.Equal(resultLength , result.Count());
            Assert.All(result, x => Assert.Equal(plannedAmount, x.PlannedAmount));
        }

        // Тест асинхронного метода для получения бюджета по запланированной сумме с невалидными данными
        [Fact]
        public async Task GetBudgetsByPlannedAmount_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 999;
            var result = await repository.GetBudgetsByPlannedAmountAsync(plannedAmount);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджета по дате окончания с валидными данными 
        [Fact]
        public async Task GetBudgetsByEndDate_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(2025, 5, 17);
            var resultLength = 4; 
            var result = await repository.GetBudgetsByEndDateAsync(endDate);
            Assert.NotEmpty(result);
            Assert.Equal(resultLength, result.Count());
            Assert.All(result, x => Assert.Equal(x.EndDate, endDate));
        }

        // Тест асинхронного метода для получения бюджетов по дате окончания с невалидными данными 
        [Fact]
        public async Task GetBudgetsByEndDate_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(9999, 9, 9);
            var result = await repository.GetBudgetsByEndDateAsync(endDate);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов по дате начала с валидными данными 
        [Fact]
        public async Task GetBudgetsByStartDate_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var resultCount = 4;
            var result = await repository.GetBudgetsByStartDateAsync(startDate);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count());
            Assert.All(result, x => Assert.Equal(startDate, x.StartDate));
        }

        // Тест асинхронного метода для получения бюджетов по дате начала с невалидными данными
        [Fact]
        public async Task GetBudgetsByStartDate_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(9999, 9, 19);
            var result = await repository.GetBudgetsByStartDateAsync(startDate);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов по дате начала и окончания с валидными данными
        [Fact]
        public async Task GetBudgetsByStartDateAndEndDate_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var endDate = new DateOnly(2025, 5, 17);
            var resultCount = 4;
            var result = await repository.GetBudgetsByStartAndEndDateAsync(startDate, endDate);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count());
            Assert.All(result, x => Assert.Equal(startDate, x.StartDate));
            Assert.All(result, x => Assert.Equal(endDate, x.EndDate));
        }

        // Тест асинхронного метода для получения бюджетов по дате начала и окончания с невалидными данными
        [Fact]
        public async Task GetBudgetsByStartDateAndEndDate_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(9999, 9, 9);
            var endDate = new DateOnly(9999, 9, 9);
            var result = await repository.GetBudgetsByStartAndEndDateAsync(startDate, endDate);
            Assert.Empty(result);
        }
        #endregion
        #region Тесты методов получения бюджетов пользователя
        // Тест асинхронного метода для получения бюджетов пользователя с валидными данными
        [Fact]
        public async Task GetUserBudgets_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var userId = 1;
            var resultCount = 4;
            var result = await repository.GetBudgetsByUserIdAsync(userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count());
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхронного метода для получения бюджетов пользователя с невалидными данными 
        [Fact]
        public async Task GetUserBudgets_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var userId = 999;
            var result = await repository.GetBudgetsByUserIdAsync(userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по id с валидными данными
        [Fact]
        public async Task GetUserBudgetsById_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var id = 1;
            var userId = 1;
            var result = await repository.GetBudgetByIdAndUserIdAsync(id, userId);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(userId, result.UserId);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по id с невалидными данными
        [Fact]
        public async Task GetUserBudgetsById_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var id = 999;
            var userId = 999;
            var result = await repository.GetBudgetByIdAndUserIdAsync(id, userId);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по id категории с валидными данными 
        [Fact]
        public async Task GetUserBudgetsByIdCategory_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 1;
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByCategoryIdAndUserIdAsync(categoryId, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(categoryId, x.CategoryId));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхроного метода для получения бюджетов пользователя по id категории с невалидными данными 
        [Fact]
        public async Task GetUserBudgetsByIdCategory_ReturnsEmpry()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 999;
            var userId = 999;
            var result = await repository.GetBudgetsByCategoryIdAndUserIdAsync(categoryId, userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по запланированной сумме с валидными данными
        [Fact]
        public async Task GetUserBudgetsPlannedAmount_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 100;
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByPlannedAmountAndUserIdAsync(plannedAmount, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count());
            Assert.All(result, x => Assert.Equal(plannedAmount, x.PlannedAmount));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по запланированной сумме с  невалидными данными
        [Fact]
        public async Task GetUserBudgetsByPlannedAnount_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 999;
            var userId = 999;
            var result = await repository.GetBudgetsByPlannedAmountAndUserIdAsync(plannedAmount, userId);
            Assert.Empty(result);
        }

        // Тест асиннхронного метода для получения бюджетов пользователя по дате начала с валидными данными
        [Fact]
        public async Task GetUserBudgetsByStartDate_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByStartDateAndUserIdAsync(startDate, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count());
            Assert.All(result, x => Assert.Equal(startDate, x.StartDate));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате начала с невалидными данными
        [Fact]
        public async Task GetUserBudgetsByStartDate_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(9999, 9, 9);
            var userId = 999;
            var result = await repository.GetBudgetsByStartDateAndUserIdAsync(startDate, userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате окончания с валидными данными
        [Fact]
        public async Task GetUserBudgetsByEndDate_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(2025, 5, 17);
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByEndDateAndUserIdAsync(endDate, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count());
            Assert.All(result, x => Assert.Equal(endDate, x.EndDate));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате окончания с невалидными данными 
        [Fact]
        public async Task GetUserBudgetsByEndDate_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(9999, 9, 9);
            var userId = 1;
            var result = await repository.GetBudgetsByEndDateAndUserIdAsync(endDate, userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате начала и окончания с валидными данными
        [Fact]
        public async Task GetUserBudgetsByStartAndEndDate_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var endDate = new DateOnly(2025, 5, 17);
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByStartAndEndDateAndUserIdAsync(startDate, endDate, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count());
            Assert.All(result, x => Assert.Equal(startDate, x.StartDate));
            Assert.All(result, x => Assert.Equal(endDate, x.EndDate));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате начала и окончания с невалидными данными
        [Fact]
        public async Task GetUserBudgetsByStartAndDate_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(9999, 9, 19);
            var endDate = new DateOnly(9999, 9, 19);
            var userId = 999;
            var result = await repository.GetBudgetsByStartAndEndDateAndUserIdAsync(startDate, endDate, userId);
            Assert.Empty(result);
        }
        #endregion
        #region Тесты методов получения бюджетов с загруженнными связанными данными
        // Тест асинхронного метода для получения бюджетов с загруженными связанными данными с валидными данными
        [Fact]
        public async Task GetAllBudgetsWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var result = await repository.GetAllBudgetsWithDetailsAsync();
            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.NotNull(x.Category));
            Assert.All(result, x => Assert.NotNull(x.User)); 
        }

        // Тест асинхронного метода для получения бюджетов с загруженными связанными данными с не валидными данными
        [Fact]
        public async Task GetAllBudgetsWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            var repository = new BudgetRepository(context);
            var result = await repository.GetAllBudgetsWithDetailsAsync();
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджета по id с загруженными связанными данными с валидными данными
        [Fact]
        public async Task GetBudgetByIdWithDetails_ReturnsBudget()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repossitory = new BudgetRepository(context);
            var id = 1;
            var result = await repossitory.GetBudgetByIdWithDetailsAsync(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(id, result.UserId);
            Assert.Equal(id, result.CategoryId);
        }

        // Тест асинхронного метода для получения бюджета по id с загруженными связанными данными с невалидными данными
        [Fact]
        public async Task GetBudgetByIdWithDetails_ReturnsNull()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var id = 99;
            var result = await repository.GetBudgetByIdWithDetailsAsync(id);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения бюджета по id категории с загруженными связанными данными с валидными данными
        [Fact]
        public async Task GetBudgetsByCategoryIdWithDetails_ReturnBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 1;
            var result = await repository.GetBudgetsByCategoryIdWithDetailsAsync(categoryId);
            var resultCount = 2;
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(categoryId, x.CategoryId));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджета по id категории с загруженными связанными данными с невалидными данными
        [Fact]
        public async Task GetBudgetsByCategorryIdWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 999;
            var result = await repository.GetBudgetsByCategoryIdAsync(categoryId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджета по запланированной сумме с загруженными связанными данными с валидными данными 
        [Fact]
        public async Task GetBudgetsByPlannedAmountWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 100;
            var resultCount = 4;
            var result = await repository.GetBudgetsByPlannedAmountWithDetailsAsync(plannedAmount);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(plannedAmount, x.PlannedAmount));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджета по запланированной сумме с загруженными связанными данными с  не валлидными данными 
        [Fact]
        public async Task GetBudgetsByPlannedAmountWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 999;
            var result = await repository.GetBudgetsByPlannedAmountWithDetailsAsync(plannedAmount);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов по дате начала с загруженными связанными данными с валидными данными
        [Fact]
        public async Task GetBudgetsByStartDateWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var resultCount = 4;
            var result =  await repository.GetBudgetsByStartDateWithDetailsAsync(startDate);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(x.StartDate, startDate));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджетов по дате начала с загруженными связанными данными с не валидными данными
        [Fact]
        public async Task GetBudgetsByStartDateWithDetails_RetursEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(9999, 9, 19);
            var result = await repository.GetBudgetsByStartDateWithDetailsAsync(startDate);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов по дате окончания с загруженными связанными данными с валидными данными 
        [Fact]
        public async Task GetBudgetsByEndDateWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(2025, 5, 17);
            var resultCount = 4;
            var result = await repository.GetBudgetsByEndDateAsync(endDate);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(endDate, x.EndDate));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджетов по дате окончания с загруженными связанными данными с не валидными данными
        [Fact]
        public async Task GetBudgetsByEndDateWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(9999, 9, 19);
            var result = await repository.GetBudgetsByEndDateWithDetailsAsync(endDate);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов по дате начала и окончания с загруженными связанными данными с валидными данными 
        [Fact]
        public async Task GetBudgetsByStartDateAndEndDateWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var endDate = new DateOnly(2025, 5, 17);
            var resultCount = 4;
            var result = await repository.GetBudgetsByStartAndEndDateWithDetailsAsync(startDate, endDate);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(startDate, x.StartDate));
            Assert.All(result, x => Assert.Equal(endDate, x.EndDate));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }
        #endregion
        #region Тесты методов для получения бюджетов пользователя с загруженными связанными данными
        // Тест асинхронного метода для получения бюджетов пользователя с загруженными связанными данными с валидными данными
        [Fact]
        public async Task GetUserBudgetsWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var userId = 1;
            var resultCount = 4;
            var result = await repository.GetBudgetsByUserIdWithDetailsAsync(userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по id с загруженными связанными данными с невалидными даннными
        [Fact]
        public async Task GetUserBudgetsWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var userId = 999;
            var result = await repository.GetBudgetsByUserIdWithDetailsAsync(userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджета пользователя по id с загруженными связанными данными с валидными данными
        [Fact]
        public async Task GetUserBudgetByIdWithDetails_ReturnsBudget()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var id = 1;
            var userId = 1;
            var result = await repository.GetBudgetByIdAndUserIdWithDetailsAsync(id, userId);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(userId, result.UserId);
            Assert.NotNull(result.User);
            Assert.NotNull(result.Category);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по id загруженными связанными данными с невалидными данными
        [Fact]
        public async Task GetUserBudgetByIdWithDetails_ReturnNull()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var id = 999;
            var userId = 999;
            var result = await repository.GetBudgetByIdAndUserIdWithDetailsAsync(id, userId);
            Assert.Null(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по id категории с загруженными связанными данными с валидными данымии
        [Fact]
        public async Task GetUserBudgetsByCategoryIdWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 1;
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByCategoryIdAndUserIdWithDetailsAsync(categoryId, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(categoryId, x.UserId));
            Assert.All(result, x => Assert.Equal(categoryId, x.CategoryId));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по id категории с загруженными связанными данными с невалидными данными
        [Fact]
        public async Task GetUserBudgetsByCategoryIdWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var categoryId = 999;
            var userId = 999;
            var result = await repository.GetBudgetsByCategoryIdAndUserIdWithDetailsAsync(categoryId, userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по запланированной сумме с загруженными связанными данными с валидными даннными
        [Fact]
        public async Task GetUserBudgetsByPlannedAmountWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 100;
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByPlannedAmountAndUserIdWithDetailsAsync(plannedAmount, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(plannedAmount, x.PlannedAmount));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по запланированной сумме с загруженными связанными данными с не валидными данными
        [Fact]
        public async Task GetUserBudgetsByPlannedAmountWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var plannedAmount = 999;
            var userId = 999;
            var result = await repository.GetBudgetsByPlannedAmountAndUserIdWithDetailsAsync(plannedAmount, userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате начала с загруженными связанными данными с валидными данными
        [Fact]
        public async Task GetUserBudgetsByStartDateWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByStartDateAndUserIdWithDetailsAsync(startDate, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(startDate, x.StartDate));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате начала с загруженными связанными данными с не валидными данными
        [Fact]
        public async Task GetUserBudgetsByStartDateWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(9999, 9, 19);
            var userId = 999;
            var result = await repository.GetBudgetsByStartDateAndUserIdWithDetailsAsync(startDate, userId);
            Assert.Empty(result);
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате окончания с загруженными связанными данными с валиидными данными
        [Fact]
        public async Task GetUserBudgetsByEndDateWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(2025, 5, 17);
            var userId = 1;
            var resultCount = 2;
            var result  = await repository.GetBudgetsByEndDateAndUserIdWithDetailsAsync(endDate, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(endDate, x.EndDate));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }

        // Тест асинхронного метода для получения бюджетов пользователя по дате окончания с загруженными связанными данными с невалидными данными
        [Fact]
        public async Task GetUserBudgetsByEndDateWithDetails_ReturnsEmpty()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var endDate = new DateOnly(9999, 9, 19);
            var userId = 1;
            var result = await repository.GetBudgetsByEndDateAndUserIdWithDetailsAsync(endDate, userId);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUserBudgetsByStartDateAndEndDateWithDetails_ReturnsBudgets()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var endDate = new DateOnly(2025, 5, 17);
            var userId = 1;
            var resultCount = 2;
            var result = await repository.GetBudgetsByStartAndEndDateAndUserIdWithDetailsAsync(startDate, endDate, userId);
            Assert.NotEmpty(result);
            Assert.Equal(resultCount, result.Count);
            Assert.All(result, x => Assert.Equal(startDate, x.StartDate));
            Assert.All(result, x => Assert.Equal(endDate, x.EndDate));
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
            Assert.All(result, x => Assert.NotNull(x.User));
            Assert.All(result, x => Assert.NotNull(x.Category));
        }
        #endregion
        #region Тест методов для добавления, обновления или удаления бюджетов с сохранением или без
        [Fact]
        public async Task AddBudgetAsync_AddsBudgetWithoutSaving()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025,4,17);    
            var endDate = new DateOnly(2025, 5, 17);
            var plannedAmount = 100;
            var newBudget = new Budget { Id = 9, PlannedAmount = plannedAmount, StartDate = startDate, EndDate = endDate };
            await repository.AddBudgetAsync(newBudget);
            var tracked = context.ChangeTracker.Entries<Budget>().FirstOrDefault(x => x.Entity.Id == newBudget.Id);
            var check = await context.Budgets.AsNoTracking().SingleOrDefaultAsync(x => x.Id == newBudget.Id);
            Assert.NotNull(tracked);
            Assert.Equal(EntityState.Added, tracked.State);
            Assert.Null(check);
        }

        [Fact]
        public async Task UpdateBudgetAsync_UpdateBudgetWithountSaving()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var newPlannedAmount = 1000;
            var budgetId = 1;
            var budgetToUpdate = await context.Budgets.SingleOrDefaultAsync(x => x.Id == budgetId);
            budgetToUpdate.PlannedAmount = newPlannedAmount;
            repository.UpdateBudget(budgetToUpdate);
            var tracked = context.ChangeTracker.Entries<Budget>().FirstOrDefault(x => x.Entity.Id == budgetId);
            var check = await context.Budgets.AsNoTracking().SingleOrDefaultAsync(x => x.Id == budgetId);
            Assert.NotNull(tracked);
            Assert.Equal(EntityState.Modified, tracked.State);
            Assert.Equal(newPlannedAmount, budgetToUpdate.PlannedAmount);

        }

        [Fact]
        public async Task DeleteBudgetAsync_DeleteBudgetWithointSaving()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var budgetId= 1;
            var budgetToDelete = await context.Budgets.SingleOrDefaultAsync(x => x.Id == budgetId);
            repository.DeleteBudget(budgetToDelete);
            var tracked = context.ChangeTracker.Entries<Budget>().FirstOrDefault(x => x.Entity.Id == budgetId);
            Assert.NotNull(tracked);
            Assert.NotNull(budgetToDelete);
            Assert.Equal(EntityState.Deleted, tracked.State);
        }

        [Fact]
        public async Task AddBudgetAndSaveAsync_AddsBudgetAndSaves()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var startDate = new DateOnly(2025, 4, 17);
            var endDate = new DateOnly(2025, 5, 17);
            var plannedAmount = 100;
            var newBudget = new Budget { Id = 9, PlannedAmount = plannedAmount, StartDate = startDate, EndDate = endDate };
            await repository.AddBudgetAndSaveAsync(newBudget);
            var check = await context.Budgets.SingleOrDefaultAsync(x => x.Id == newBudget.Id);
            Assert.NotNull(check);
        }

        [Fact]
        public async Task UpdateBudgetAndSaveAsync_UpdateBudgetAndSaves()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var newPlannedAmount = 1000;
            var budgetId = 1;
            var budgetToUpdate = await context.Budgets.SingleOrDefaultAsync(x => x.Id == budgetId);
            budgetToUpdate.PlannedAmount = newPlannedAmount;
            await repository.UpdateBudgetAndSaveAsync(budgetToUpdate);
            var check = await context.Budgets.SingleOrDefaultAsync(x => x.Id == budgetId);
            Assert.NotNull(check);
            Assert.Equal(newPlannedAmount, check.PlannedAmount);
        }

        [Fact]
        public async Task DeleteBudgetAndSaveAsync_DeleteBudgetAndSaves()
        {
            using var context = CreateContext();
            await SeedData(context);
            var repository = new BudgetRepository(context);
            var budgetId = 1;
            var budgetToDelete = await context.Budgets.SingleOrDefaultAsync(x => x.Id == budgetId);
            await repository.DeleteBudgetAndSaveAsync(budgetToDelete);
            var check = await context.Budgets.SingleOrDefaultAsync(x => x.Id == budgetId);
            Assert.Null(check);
        }
        #endregion
    }
}

//public Task<Budget?> GetBudgetByIdAndUserIdWithDetailsAsync(int id, int userId);
//public Task<List<Budget>> GetBudgetsByUserIdWithDetailsAsync(int userId);
//public Task<List<Budget>> GetBudgetsByCategoryIdAndUserIdWithDetailsAsync(int categoryId, int userId);
//public Task<List<Budget>> GetBudgetsByPlannedAmountAndUserIdWithDetailsAsync(decimal plannedAmount, int userId);
//public Task<List<Budget>> GetBudgetsByStartDateAndUserIdWithDetailssAsync(DateOnly startDate, int userId);
//public Task<List<Budget>> GetBudgetsByEndDateAndUserIdWithDetailsAsync(DateOnly endDate, int userId);
//public Task<List<Budget>> GetBudgetsByStartAndEndDateAndUserIdWithDetailsAsync(DateOnly startDate, DateOnly endDate, int userId);
//public Task AddBudgetAsync(Budget budgetToAdd);
//public void UpdateBudget(Budget budgetToUpdate);
//public void DeleteBudget(Budget budgetToDelete);
//public Task AddBudgetAndSaveAsync(Budget budgetToAdd);
//public Task UpdateBudgetAndSaveAsync(Budget budgetToUpdate);
//public Task DeleteBudgetAndSaveAsync(Budget budgetToDelete);