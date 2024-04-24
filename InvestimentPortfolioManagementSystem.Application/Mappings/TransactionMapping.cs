using InvestimentPortfolioManagementSystem.Application.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TransactionType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.ProductValue)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.TransactionTimeStamp)
                .IsRequired()
                .HasColumnType("datetime");

            // Table Name to map these columns
            builder.ToTable("Transactions");
        }
    }
}
