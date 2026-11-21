using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class ReceiptItemConfiguration : IEntityTypeConfiguration<ReceiptItem>
    {
        public void Configure(EntityTypeBuilder<ReceiptItem> builder)
        {
            builder.ToTable("ReceiptItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(40)
                   .IsRequired();
            builder.Property(x => x.Quantity)
                   .HasColumnName("Quantity");
            builder.Property(x => x.UnitPrice)
                   .HasColumnName("UnitPrice");
            builder.Ignore(x => x.TotalPrice);
            builder.HasOne(x => x.Tag)
                   .WithMany(x => x.ReceiptItems)
                   .HasForeignKey(x => x.TagId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.ReceiptItems)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Receipt)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.ReceiptId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}