using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class WorkDayRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfReceivingAllWorkDays()
        {
            var count = 3;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var workDay1 = new WorkDay { Id = 1, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            var workDay2 = new WorkDay { Id = 2, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            var workDay3 = new WorkDay { Id = 3, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            await context.AddRangeAsync(workDay1, workDay2, workDay3);
            await context.SaveChangesAsync();
            var result = await repository.GetAll();
            Assert.Equal(count, result.Count);
        }

        [Fact]
        public async Task TheTestOfGettingAllNonExistingWorkDays()
        {
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var result = await repository.GetAll();
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingWorkDayById()
        {
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var workDay1 = new WorkDay { Id = 1, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            var workDay2 = new WorkDay { Id = 2, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            var workDay3 = new WorkDay { Id = 3, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            await context.AddRangeAsync(workDay1, workDay2, workDay3);
            await context.SaveChangesAsync();
            var result = await repository.GetById(workDay1.Id);
            Assert.NotNull(result);
            Assert.Equal(workDay1.Id, result.Id);
        }

        [Fact]
        public async Task TestForReceivingAWorkDayForANonExistentId()
        {
            var id = 999;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var result = await repository.GetById(id);
            Assert.Null(result);
        }


        [Fact]
        public async Task TheTestOfGettingWorkDayByPlaseOfWorkId()
        {
            var count = 2;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var plaseOfWork1 = new PlaceOfWork { Id = 1, Company = "Company1", Division = "Division1", Post = "post1", ServiceNumber = "serviceNumber1" };
            var plaseOfWork2 = new PlaceOfWork { Id = 2, Company = "Company2", Division = "Division2", Post = "post2", ServiceNumber = "serviceNumber2" };
            var workDay1 = new WorkDay { Id = 1, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1, PlaseOfWorkId = plaseOfWork1.Id, PlaseOfWork = plaseOfWork1};
            var workDay2 = new WorkDay { Id = 2, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1, PlaseOfWorkId = plaseOfWork1.Id, PlaseOfWork = plaseOfWork1 };
            var workDay3 = new WorkDay { Id = 3, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            await context.WorkDays.AddRangeAsync(workDay1, workDay2, workDay3);
            await context.PlaceOfWorks.AddRangeAsync(plaseOfWork1, plaseOfWork2);
            await context.SaveChangesAsync();
            var result = await repository.GetByPlaseOfWorkId(workDay1.PlaseOfWorkId);
            Assert.Equal(count, result.Count);
            Assert.All(result, x => Assert.Equal(plaseOfWork1.Id, x.PlaseOfWorkId));
            Assert.All(result, x => Assert.Equal(plaseOfWork1, x.PlaseOfWork));
        }

        [Fact]
        public async Task TestForReceivingAWorkDayForANonExistentPlaseOfWorkId()
        {
            var id = 999;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var result = await repository.GetByPlaseOfWorkId(id);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingWorkDayByBillingStatementId()
        {
            var count = 2;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var fixedDate = new DateTime(2026, 07, 30, 12, 0, 0);
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            var billingStatement2 = new PayStub { Id = 2, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            var workDay1 = new WorkDay { Id = 1, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1, BillingStatementId = billingStatement1.Id, PayStubs = billingStatement1 };
            var workDay2 = new WorkDay { Id = 2, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1, BillingStatementId = billingStatement1.Id, PayStubs = billingStatement1 };
            var workDay3 = new WorkDay { Id = 3, Date = fixedDate, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1, BillingStatementId = billingStatement2.Id, PayStubs = billingStatement2 };
            await context.PayStubs.AddRangeAsync(billingStatement1, billingStatement2);
            await context.WorkDays.AddRangeAsync(workDay1, workDay2, workDay3);
            await context.SaveChangesAsync();
            var result = await repository.GetByBillingStatementId(billingStatement1.Id);
            Assert.Equal(count, result.Count);
            Assert.All(result, x => Assert.Equal(billingStatement1.Id, x.BillingStatementId));
            Assert.All(result, x => Assert.Equal(billingStatement1, x.PayStubs));
        }

        [Fact]
        public async Task TestForReceivingAWorkDayForANonExistentBillingStatementId()
        {
            var id = 999;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var result = await repository.GetByBillingStatementId(id);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingWorkDayByDate()
        {
            var count = 2;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var date = new DateTime(2026, 07, 30, 12, 0, 0);
            var date2 = new DateTime(2026, 08, 29, 12, 0, 0);
            var workDay1 = new WorkDay { Id = 1, Date = date, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            var workDay2 = new WorkDay { Id = 2, Date = date, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            var workDay3 = new WorkDay { Id = 3, Date = date2, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            await context.AddRangeAsync(workDay1, workDay2, workDay3);
            await context.SaveChangesAsync();
            var result = await repository.GetByDate(date);
            Assert.Equal(count, result.Count);
            Assert.All(result, x => Assert.Equal(date, x.Date));
        }

        [Fact]
        public async Task TestForReceivingAWorkDayForANonExistentDate()
        {
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var date = new DateTime(2026, 07, 30, 12, 0, 0);
            var result = await repository.GetByDate(date);
            Assert.Empty(result);
        }

        [Fact]
        public async Task WorkDayAdditionTest()
        {
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var date = new DateTime(2026, 07, 30, 12, 0, 0);
            var workDay = new WorkDay { Id = 1, Date = date, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            await repository.AddAndSave(workDay);
            var result = context.WorkDays.FindAsync(workDay.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task WorkDayUpdateTest()
        {
            short newHourlyRate = 420;
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var date = new DateTime(2026, 07, 30, 12, 0, 0);
            var workDay = new WorkDay { Id = 1, Date = date, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            await context.WorkDays.AddAsync(workDay);
            await context.SaveChangesAsync();
            workDay.HourlyRate = newHourlyRate;
            await repository.UpdateAndSave(workDay);
            var result = await context.WorkDays.FindAsync(workDay.Id);
            Assert.NotNull(result);
            Assert.Equal(newHourlyRate, result.HourlyRate);
        }

        [Fact]
        public async Task WorkDayDeleteTest()
        {
            var context = CreateContext();
            var repository = new WorkDayRepository(context);
            var date = new DateTime(2026, 07, 30, 12, 0, 0);
            var workDay = new WorkDay { Id = 1, Date = date, HourlyRate = 320, TotalWorkHours = 9, BreakHours = 1 };
            await context.WorkDays.AddAsync(workDay);
            await context.SaveChangesAsync();
            await repository.DeleteAndSave(workDay);
            var result = await context.WorkDays.FindAsync(workDay.Id);
            Assert.Null(result);
        }
    }
}

