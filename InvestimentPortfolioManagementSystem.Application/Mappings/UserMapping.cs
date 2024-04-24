using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestimentPortfolioManagementSystem.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestimentPortfolioManagementSystem.Application.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.EmailAddress)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false); // Emails usually consist of ASCII characters only

            // Ensure that Email Addresses are unique
            builder.HasIndex(p => p.EmailAddress)
                .IsUnique();

            builder.Property(p => p.UserType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.RegistrationDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            // 1 : N => User : Products
            builder.HasMany(f => f.Products)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            // 1 : N => User : Transactions
            builder.HasMany(f => f.Transactions)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            // Table Name to map these columns
            builder.ToTable("Users");
        }
    }
}
