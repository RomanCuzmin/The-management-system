using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.ToTable("Receipts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Date)
                   .HasColumnName("Date")
                   .IsRequired();
            builder.Property(x => x.StoreName)
                   .HasColumnName("StoreName")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(x => x.TotalAmount)
                   .HasColumnName("TotalAmount")
                   .IsRequired();
            builder.Property(x => x.PaymentMethod)
                   .HasColumnName("PaymentMethod")
                   .HasConversion<string>()
                   .IsRequired();
            builder.Property(x => x.Notes)
                   .HasColumnName("Notes")
                   .HasMaxLength(50)
                   .IsRequired(false);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Receipts)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Receipts)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Items)
                   .WithOne(x => x.Receipt)
                   .HasForeignKey(x => x.ReceiptId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
