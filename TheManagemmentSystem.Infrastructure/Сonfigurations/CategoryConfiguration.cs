using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(40)
                   .IsRequired();
            builder.Property(x => x.Type)
                   .HasColumnName("Type")
                   .HasConversion<string>();
            builder.HasMany(x => x.Tags)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Transactions)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Budgets)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.BillingStatements)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
        //public string Name { get; set; }
        //public TransactionType Type { get; set; }
        //public int? ParentCategoryId { get; set; }
        //public Category ParentCategory { get; set; }
        //public ICollection<Category> SubCategories { get; set; } = new List<Category>();
        //public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        //public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        //public ICollection<BillingStatement> BillingStatements { get; set; } = new List<BillingStatement>();
    }
    }
