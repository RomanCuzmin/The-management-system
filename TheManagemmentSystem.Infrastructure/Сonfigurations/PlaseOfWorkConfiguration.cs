using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class PlaseOfWorkConfiguration : IEntityTypeConfiguration<PlaseOfWork>
    {
        public void Configure(EntityTypeBuilder<PlaseOfWork> builder)
        {
            builder.ToTable("PlaseOfWorks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Company)
                   .HasColumnName("Company")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(x => x.Division)
                   .HasColumnName("Division")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(x => x.Post)
                   .HasColumnName("Post")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(x => x.ServiceNumber)
                   .HasColumnName("ServiceNumber")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasOne(x => x.User)
                   .WithMany(x => x.PlaseOfWorks)
                   .HasForeignKey(x => x.User.Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.BillingStatements)
                   .WithOne(x => x.PlaseOfWork)
                   .HasForeignKey(x => x.PlaseOfWorkId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.WorkDays)
                   .WithOne(x => x.PlaseOfWork)
                   .HasForeignKey(x => x.PlaseOfWorkId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
