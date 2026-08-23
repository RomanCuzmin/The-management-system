using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class BillingStatementRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfReceivingAllPayslips()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var user = new User { Id = 1, Email = "testuser@email.com", Password = "password", NotificationType = NotificationType.All, PhoneNumber = "89200798900" };
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100, UserId = user.Id, User = user };
            var billingStatement2 = new PayStub { Id = 2, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100, UserId = user.Id, User = user };
            var billingStatement3 = new PayStub { Id = 3, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100, UserId = user.Id, User = user };
            await context.Users.AddAsync(user);
            await context.PayStubs.AddRangeAsync( billingStatement1, billingStatement2, billingStatement3 );
            await context.SaveChangesAsync();
            var result = await repository.CetAll(user.Id);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task TestForReceivingAllCalculationSheetsWithInvalidData()
        {
            var userId = 999;
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var result = await repository.CetAll(userId);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TestForReceivingAllThePayslipsById()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var user = new User { Id = 1, Email = "testuser@email.com", Password = "password", NotificationType = NotificationType.All, PhoneNumber = "89200798900" };
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            var billingStatement2 = new PayStub { Id = 2, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            var billingStatement3 = new PayStub { Id = 3, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            await context.Users.AddAsync(user);
            await context.PayStubs.AddRangeAsync(billingStatement1, billingStatement2, billingStatement3);
            await context.SaveChangesAsync();
            var result = await repository.GetByIdAndUserIdWithDetails(user.Id, billingStatement1.Id);
            Assert.NotNull(result);
            Assert.Equal(billingStatement1.Id, result.Id);
        }

        [Fact]
        public async Task TestForReceivingAllPayslipsByIdWithInvalidData()
        {
            var id = 999;
            var userId = 999;
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var result = await repository.GetByIdAndUserIdWithDetails(userId, id);
            Assert.Null(result);
        }

        [Fact]
        public async Task TestForReceivingAPayslipByJobId()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var user = new User { Id = 1, Email = "testuser@email.com", Password = "password", NotificationType = NotificationType.All, PhoneNumber = "89200798900" };
            var plaseOfWork1 = new PlaceOfWork { Id = 1, Company = "company1", Division = "division1", Post = "post1", ServiceNumber = "serviceNumber1" };
            var plaseOfWork2 = new PlaceOfWork { Id = 2, Company = "company2", Division = "division2", Post = "post2", ServiceNumber = "serviceNumber2" };
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100, PlaseOfWorkId = plaseOfWork1.Id, PlaseOfWork = plaseOfWork1, UserId = user.Id, User = user };
            var billingStatement2 = new PayStub { Id = 2, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100, PlaseOfWorkId = plaseOfWork1.Id, PlaseOfWork = plaseOfWork1, UserId = user.Id, User = user };    
            var billingStatement3 = new PayStub { Id = 3, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100, PlaseOfWorkId = plaseOfWork2.Id, PlaseOfWork = plaseOfWork2, UserId = user.Id, User = user };
            await context.Users.AddAsync(user);
            await context.PlaceOfWorks.AddRangeAsync( plaseOfWork1, plaseOfWork2 );
            await context.PayStubs.AddRangeAsync(billingStatement1, billingStatement2, billingStatement3);
            await context.SaveChangesAsync();
            var result = await repository.GetByPlaseOfWorkIdAndUserIdWithDetails(user.Id ,plaseOfWork1.Id);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(plaseOfWork1.Id, x.PlaseOfWorkId));
            Assert.All(result, x => Assert.Equal(plaseOfWork1, x.PlaseOfWork));
        }

        [Fact]
        public async Task TestForReceivingAPayslipWithAnInvalidWorkplaceId()
        {
            var userId = 999;
            var plaseOfWorkId = 999;
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var result = await repository.GetByPlaseOfWorkIdAndUserIdWithDetails(userId, plaseOfWorkId);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TestForReceivingAPayslipByUserId()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var user1 = new User { Id = 1, Email = "email1@.com", Password = "password1", PhoneNumber = "89300697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "email2@.com", Password = "password2", PhoneNumber = "89400798900", NotificationType = NotificationType.All };
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100, UserId = user1.Id, User = user1 };
            var billingStatement2 = new PayStub { Id = 2, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100,  UserId = user1.Id, User = user1 };
            var billingStatement3 = new PayStub { Id = 3, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 , UserId = user2.Id, User = user2 };
            await context.Users.AddRangeAsync(user1, user2);
            await context.PayStubs.AddRangeAsync(billingStatement1, billingStatement2, billingStatement3);
            await context.SaveChangesAsync();
            var result = await repository.GetByUserIdWithDetails(user1.Id);
            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal(user1.Id, x.UserId));
            Assert.All(result, x => Assert.Equal(user1, x.User));
        }

        [Fact]
        public async Task TestForReceivingAPayslipWithAnInvalidUserId()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var result = await repository.GetByUserIdWithDetails(999);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfAddingAPayslip()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            await repository.AddAndSave(billingStatement1);
            var result = await context.PayStubs.FindAsync(billingStatement1.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TheCheckSheetUpdateTest()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            await context.AddAsync(billingStatement1);
            await context.SaveChangesAsync();
            billingStatement1.IncomeTax = 200;
            await repository.UpdateAndSave(billingStatement1);
            var result = await context.PayStubs.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal(200, result.IncomeTax);
        }

        [Fact]
        public async Task TheCheckSheetDeletionTest()
        {
            var context = CreateContext();
            var repository = new BillingStatementRepository(context);
            var billingStatement1 = new PayStub { Id = 1, BaseSalary = 100, Bonus = 100, OtherAccruals = 100, IncomeTax = 100, PensionContribution = 100, OtherDeductions = 100 };
            await context.AddAsync(billingStatement1);
            await context.SaveChangesAsync();
            await repository.DeleteAndSave(billingStatement1);
            var result = await context.PayStubs.FindAsync(1);
            Assert.Null(result);
        }
    }
}
