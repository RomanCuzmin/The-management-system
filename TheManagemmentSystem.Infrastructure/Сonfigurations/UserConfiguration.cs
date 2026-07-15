using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Email)
                   .HasColumnName("Email")
                   .HasMaxLength(40)
                   .IsRequired();
            builder.Property(x => x.Password)
                   .HasColumnName("Password")
                   .HasMaxLength(18)
                   .IsRequired();
            builder.Property(x => x.PhoneNumber)
                   .HasColumnName("PhoneNumber")
                   .HasMaxLength(15)
                   .IsRequired();
            builder.Property(x => x.NotificationType)
                   .HasColumnName("NotificationType")
                   .HasConversion<string>();
            builder.HasOne(x => x.PersonalData)
                   .WithOne(x => x.User)
                   .HasForeignKey<User>(x => x.PersonalDataId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.PlaseOfWorks)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Accounts)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Budgets)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.BillingStatements)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
       
    }
}
