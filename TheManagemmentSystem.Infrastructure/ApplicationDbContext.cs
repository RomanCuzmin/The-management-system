using Microsoft.EntityFrameworkCore;
using TheManagementSystem.Domain.Entities;


namespace TheManagemmentSystem.Infrastructure
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<BillingStatement> BillingStatements => Set<BillingStatement>();
        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();   
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<User> Users => Set<User>();
        public DbSet<WorkDay> WorkDays => Set<WorkDay>();
        public DbSet<PlaseOfWork> PlaseOfWorks => Set<PlaseOfWork>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    }
}
