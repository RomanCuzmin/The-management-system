using Microsoft.EntityFrameworkCore;
using TheManagementSystem.Domain.Entities;
using Task = TheManagementSystem.Domain.Entities.Task;

namespace TheManagemmentSystem.Infrastructure
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<BillingStatement> BillingStatements => Set<BillingStatement>();
        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();   
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Worker> Workers => Set<Worker>();

    }
}
