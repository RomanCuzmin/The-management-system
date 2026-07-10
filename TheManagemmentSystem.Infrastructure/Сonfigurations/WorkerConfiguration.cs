using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("Workers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(30)
                   .IsRequired();
            builder.Property(x => x.SurName)
                   .HasColumnName("SurName")
                   .HasMaxLength(30)
                   .IsRequired();
            builder.Property(x => x.Patronymic)
                   .HasColumnName("Patronymic")
                   .HasMaxLength(30)
                   .IsRequired();
            builder.Property(x => x.PlaseOfWork)
                   .HasColumnName("PlaseOfWork")
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
                   .WithOne(x => x.Worker)
                   .HasForeignKey<Worker>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.WorkDays)
                   .WithOne("WorkDays")
                   .HasForeignKey(x => x.WorkerId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.BillingStatements)
                   .WithOne(x => x.Worker)
                   .HasForeignKey(x => x.WorkerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

