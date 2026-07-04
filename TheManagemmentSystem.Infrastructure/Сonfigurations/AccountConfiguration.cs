using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(32)
                   .IsRequired();
            builder.Property(x => x.Type)
                   .HasColumnName("Type")
                   .HasConversion<string>()
                   .IsRequired();
            builder.Property(x => x.InitialBalance)
                   .HasColumnName("InitialBalance")
                   .IsRequired();
            builder.Property(x => x.Currency)
                   .HasColumnName("Currency")
                   .HasMaxLength(6)
                   .IsRequired();
            builder.Ignore(x => x.Balance);
            builder.HasMany(x => x.Transactions)
                   .WithOne(x => x.Account)
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Transactions)
                   .WithOne(x => x.ToAccount)
                   .HasForeignKey(x => x.ToAccountId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Accounts)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
