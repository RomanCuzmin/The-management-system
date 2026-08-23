using Microsoft.EntityFrameworkCore;
using TheManagementSystem.Domain.Entities;
using TheManagemmentSystem.Infrastructure;
using TheManagemmentSystem.Infrastructure.Repositories.Implementations;

namespace TheManagementSystem.Tests.RepositoryTests
{
    public class TagRepositoryTest
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task TheTestOfGettingAllTheTags()
        {
            var resultCount = 3;
            var context = CreateContext();
            var repository = new TagRepository(context);
            var tag1 = new Tag { Id = 1, Name = "name1" };
            var tag2 = new Tag { Id = 2, Name = "name2" };
            var tag3 = new Tag { Id = 3, Name = "name3" };
            await context.Tags.AddRangeAsync(tag1, tag2, tag3);
            await context.SaveChangesAsync();
            var result = await repository.GetAll();
            Assert.Equal(resultCount, result.Count);
        }

        [Fact]
        public async Task TheTestOfGettingAllNonExistentTags()
        {
            var context = CreateContext();
            var repository = new TagRepository(context);
            var result = await repository.GetAll();
            Assert.Empty(result);
        }

        [Fact]
        public async Task TheTestOfGettingTagsById()
        {
            var context = CreateContext();
            var repository = new TagRepository(context);
            var tag1 = new Tag { Id = 1, Name = "name1" };
            var tag2 = new Tag { Id = 2, Name = "name2" };
            var tag3 = new Tag { Id = 3, Name = "name3" };
            await context.AddRangeAsync(tag1, tag2, tag3);
            await context.SaveChangesAsync();
            var result = repository.GetById(tag1.Id);
            Assert.NotNull(result);
            Assert.Equal(tag1.Id, result.Id);
        }

        [Fact]
        public async Task TestForReceivingABudgetForANonExistentId()
        {
            var nonExistentId = 999;
            var context = CreateContext();
            var repository = new TagRepository(context);
            var result = await repository.GetById(nonExistentId);
            Assert.Null(result);
        }

        [Fact]
        public async Task TheTestOfGettingTagsByName()
        {
            var context = CreateContext();
            var repository = new TagRepository(context);
            var tag1 = new Tag { Id = 1, Name = "name1" };
            var tag2 = new Tag { Id = 2, Name = "name2" };
            var tag3 = new Tag { Id = 3, Name = "name3" };
            await context.AddRangeAsync(tag1, tag2, tag3);
            await context.SaveChangesAsync();
            var result = await repository.GetByName(tag1.Name);
            Assert.NotNull(result);
            Assert.Equal(tag1.Name, result.Name);
        }

        [Fact]
        public async Task TestForReceivingABudgetForANonExistentName()
        {
            var nonExistentName = "999";
            var context = CreateContext();
            var repository = new TagRepository(context);
            var result = await repository.GetByName(nonExistentName);
            Assert.Null(result);
        }

        [Fact]
        public async Task TagAdditionTest()
        {
            var context = CreateContext();
            var repository = new TagRepository(context);
            var tag1 = new Tag { Id = 1, Name = "name1" };
            await repository.AddAndSave(tag1);
            var result = context.Tags.FindAsync(tag1.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TagUpdateTest()
        {
            var context = CreateContext();
            var repository = new TagRepository(context);
            var tag1 = new Tag { Id = 1, Name = "name1" };
            await context.Tags.AddAsync(tag1);
            await context.SaveChangesAsync();
            tag1.Name = "test";
            await repository.UpdateAndSave(tag1);
            var result = await context.Tags.FindAsync(tag1.Id);
            Assert.NotNull(result);
            Assert.Equal("test", result.Name);
        }

        [Fact]
        public async Task TagDeleteTest()
        {
            var context = CreateContext();
            var repository = new TagRepository(context);
            var tag1 = new Tag { Id = 1, Name = "name1" };
            context.Tags.Add(tag1);
            await context.SaveChangesAsync();
            await repository.DeleteAndSave(tag1);
            var result = await context.Tags.FindAsync(tag1.Id);
            Assert.Null(result);
        }
    }
}
