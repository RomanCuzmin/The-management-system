using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class BillingStatementConfiguration : IEntityTypeConfiguration<BillingStatement>
    {
        public void Configure(EntityTypeBuilder<BillingStatement> builder)
        {
            builder.ToTable("BillingStatements");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.BaseSalary)
                   .HasColumnName("BaseSalary");
            builder.Property(x => x.Bonus)
                   .HasColumnName("Bonus");
            builder.Property(x => x.OtherAccruals)
                   .HasColumnName("OtherAccruals");
            builder.Property(x => x.IncomeTax)
                   .HasColumnName("IncomeTax");
            builder.Property(x => x.PensionContribution)
                   .HasColumnName("PensionContribution");
            builder.Property(x => x.OtherDeductions)
                   .HasColumnName("OtherDeductions");
            builder.Ignore(x => x.TotalAccrued);
            builder.Ignore(x => x.TotalDeducted);
            builder.Ignore(x => x.NetAmount);
            builder.HasOne(x => x.PlaseOfWork)
                   .WithMany(x => x.BillingStatements)
                   .HasForeignKey(x => x.PlaseOfWorkId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.BillingStatements)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Account)
                   .WithMany(x => x.BillingStatements)
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Category)
                   .WithMany(x => x.BillingStatements)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Transaction)
                   .WithMany(x => x.BillingStatements)
                   .HasForeignKey(x => x.TransactionId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.WorkDays)
                   .WithOne(x => x.BillingStatement)
                   .HasForeignKey(x => x.BillingStatementId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
