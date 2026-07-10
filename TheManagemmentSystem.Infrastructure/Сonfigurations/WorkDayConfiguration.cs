using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagemmentSystem.Infrastructure.Сonfigurations
{
    public class WorkDayConfiguration : IEntityTypeConfiguration<WorkDay>
    {
        public void Configure(EntityTypeBuilder<WorkDay> builder)
        {
            builder.ToTable("WorkDays");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .UseIdentityColumn();
            builder.Property(x => x.Date)
                   .HasColumnName("Date");
            builder.Property(x => x.HourlyRate)
                   .HasColumnName("HourlyRate");
            builder.Property(x => x.TotalWorkHours)
                   .HasColumnName("TotalWorkHours");
            builder.Property(x => x.BreakHours)
                   .HasColumnName("BreakHours");
            builder.Property(x => x.TaxRate)
                   .HasColumnName("TaxRate");
            builder.Ignore(x => x.PaidHours);
            builder.Ignore(x => x.GrossEarnings);
            builder.Ignore(x => x.TaxAmount);
            builder.Ignore(x => x.NetEarnings);
            builder.HasOne(x => x.Worker)
                   .WithMany(x => x.WorkDays)
                   .HasForeignKey(x => x.WorkerId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.BillingStatement)
                   .WithMany(x => x.WorkDays)
                   .HasForeignKey(x => x.BillingStatementId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

