using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Amount)
                   .HasColumnName("Amount");
            builder.Property(x => x.Date)
                   .HasColumnName("Date");
            builder.Property(x => x.Description)
                   .HasColumnName("Descriprion")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(x => x.TransactionType)
                   .HasColumnName("TransactionType")
                   .HasConversion<string>();
            builder.HasOne(x => x.Account)
                   .WithMany(x => x.Transactions)
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ToAccount)
                   .WithMany(x => x.Transactions)
                   .HasForeignKey(x => x.ToAccountId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Transactions)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.BillingStatements)
                   .WithOne(x => x.Transaction)
                   .OnDelete(DeleteBehavior.Restrict);
        }
        //public int Id { get; set; }
        //public decimal Amount { get; set; }
        //public DateTime Date { get; set; }
        //public string Description { get; set; }
        //public TransactionType TransactionType { get; set; }
        //public int AccountId { get; set; }
        //public int? ToAccountId { get; set; }
        //public int? CategoryId { get; set; }
        //public Account Account { get; set; }
        //public Account ToAccount { get; set; }
        //public Category Category { get; set; }
        //public List<BillingStatement> BillingStatements { get; set; } = new List<BillingStatement>();
    }
}
