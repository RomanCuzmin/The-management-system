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
    public class UserRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfReceivingAllUsers()
        {
            var resultCount = 3;
            var context = CreateContext();
            var repository = new UserRepository(context);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password1", PhoneNumber = "+79200697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "user2@email.com", Password = "password2", PhoneNumber = "+79200798900", NotificationType = NotificationType.All };
            var user3 = new User { Id = 3, Email = "user3@email.com", Password = "password3", PhoneNumber = "+79200899900", NotificationType = NotificationType.All };
            await  context.Users.AddRangeAsync(user1, user2, user3);
            await context.SaveChangesAsync();
            var result = await repository.GetAll();
            Assert.Equal(resultCount, result.Count);
        }

        [Fact]
        public async Task TheTestOfGettingAllNonExistingUsers()
        {
            var context =  CreateContext();
            var repository = new UserRepository(context);
            var result = await repository.GetAll();
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingUserById()
        {
            var context = CreateContext();
            var repository = new UserRepository(context);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password1", PhoneNumber = "+79200697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "user2@email.com", Password = "password2", PhoneNumber = "+79200798900", NotificationType = NotificationType.All };
            var user3 = new User { Id = 3, Email = "user3@email.com", Password = "password3", PhoneNumber = "+79200899900", NotificationType = NotificationType.All };
            await context.Users.AddRangeAsync(user1, user2, user3);
            await context.SaveChangesAsync();
            var result = await repository.GetById(user1.Id);
            Assert.NotNull(result);
            Assert.Equal(user1.Id, result.Id);
        }

        [Fact]
        public async Task TestForReceivingAUserForANonExistentId()
        {
            var nonExistentId = 1;
            var context = CreateContext();
            var repository = new UserRepository(context);
            var result = await repository.GetById(nonExistentId);
            Assert.Null(result);
        }


        [Fact]
        public async Task TheTestOfGettingUserByEmail()
        {
            var context = CreateContext();
            var repository = new UserRepository(context);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password1", PhoneNumber = "+79200697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "user2@email.com", Password = "password2", PhoneNumber = "+79200798900", NotificationType = NotificationType.All };
            var user3 = new User { Id = 3, Email = "user3@email.com", Password = "password3", PhoneNumber = "+79200899900", NotificationType = NotificationType.All };
            await context.Users.AddRangeAsync(user1, user2, user3);
            await context.SaveChangesAsync();
            var result = await repository.GetByEmail(user1.Email);
            Assert.NotNull(result);
            Assert.Equal(user1.Email, result.Email);
        }


        [Fact]
        public async Task TestForReceivingAUserForANonExistentEmail()
        {
            var email = "email@.com";
            var context = CreateContext();
            var repository = new UserRepository(context);
            var result = await repository.GetByEmail(email);
            Assert.Null(result);
        }


        [Fact]
        public async Task TheTestOfGettingUserByEmailAndPassword()
        {
            var context = CreateContext();
            var repository = new UserRepository(context);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password1", PhoneNumber = "+79200697900", NotificationType = NotificationType.All };
            var user2 = new User { Id = 2, Email = "user2@email.com", Password = "password2", PhoneNumber = "+79200798900", NotificationType = NotificationType.All };
            var user3 = new User { Id = 3, Email = "user3@email.com", Password = "password3", PhoneNumber = "+79200899900", NotificationType = NotificationType.All };
            await context.Users.AddRangeAsync(user1, user2, user3);
            await context.SaveChangesAsync();
            var result = await repository.GetByEmailAndPassword(user1.Email, user1.Password);
            Assert.NotNull(result);
            Assert.Equal(user1.Email, result.Email);
            Assert.Equal(user1.Password, result.Password);
        }


        [Fact]
        public async Task TestForReceivingAUserForANonExistentEmailAndPassword()
        {
            var email = "email@.com";
            var password = "password";
            var context = CreateContext();
            var repository = new UserRepository(context);
            var result = await repository.GetByEmail(email);
            Assert.Null(result);
        }

        [Fact]
        public async Task UserAdditionTest()
        {
            var context = CreateContext();
            var repository = new UserRepository(context);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password1", PhoneNumber = "+79200697900", NotificationType = NotificationType.All };
            await repository.AddAndSave(user1);
            var result = await context.Users.FindAsync(user1.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UserUpdateTest()
        {
            var context = CreateContext();
            var repository = new UserRepository(context);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password1", PhoneNumber = "+79200697900", NotificationType = NotificationType.All };
            await context.Users.AddAsync(user1);
            await context.SaveChangesAsync();
            user1.NotificationType = NotificationType.PushNotifications;
            await repository.UpdateAndSave(user1);
            var result = await context.Users.FindAsync(user1.Id);
            Assert.NotNull(result);
            Assert.Equal(NotificationType.PushNotifications, result.NotificationType);
        }

        [Fact]
        public async Task UserDeleteTest()
        {
            var context = CreateContext();
            var repository = new UserRepository(context);
            var user1 = new User { Id = 1, Email = "user1@email.com", Password = "password1", PhoneNumber = "+79200697900", NotificationType = NotificationType.All };
            await context.Users.AddAsync(user1);
            await context.SaveChangesAsync();
            await repository.DeleteAndSave(user1);
            var result = await context.Users.FindAsync(user1.Id);
            Assert.Null(result);
        }
    } 
}
