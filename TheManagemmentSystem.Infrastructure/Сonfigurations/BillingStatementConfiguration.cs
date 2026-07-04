using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        }
    }
}
//public int Id { get; set; }
//public int WorkerId { get; set; }
//public int UserId { get; set; }
//public string Accured { get; set; }
//public string Withheld { get; set; }
//public string PaidOut { get; set; }
//public DateOnly BeginningOfThePeriod { get; set; }
//public DateOnly EndOfThePeriod { get; set; }
//public Worker Worker { get; set; }
//public User User { get; set; }