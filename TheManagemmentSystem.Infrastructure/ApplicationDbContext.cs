using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TheManagementSystem.Domain.Entities;
using TheManagemmentSystem.Infrastructure.Сonfigurations;


namespace TheManagemmentSystem.Infrastructure
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Tag> Tags => Set<Tag>();   
        public DbSet<User> Users => Set<User>();
        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<WorkDay> WorkDays => Set<WorkDay>();
        public DbSet<Receipt> Receipts => Set<Receipt>();
        public DbSet<ReceiptItem> ReceiptItems => Set<ReceiptItem>();
        public DbSet<PlaceOfWork> PlaceOfWorks => Set<PlaceOfWork>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<PersonalData> PersonalData => Set<PersonalData>();
        public DbSet<PayStub> PayStubs => Set<PayStub>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
