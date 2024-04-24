using InvestimentPortfolioManagementSystem.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ExpirationDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.RegistrationDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(p => p.IsAvailableForSell)
                .IsRequired()
                .HasColumnType("bit");

            // 1 : N => Product : Transactions
            builder.HasMany(f => f.Transactions)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            // Table Name to map these columns
            builder.ToTable("Products");

        }
    }
}
