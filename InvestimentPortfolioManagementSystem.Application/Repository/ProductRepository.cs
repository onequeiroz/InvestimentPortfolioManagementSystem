using InvestimentPortfolioManagementSystem.Application.Context;
using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await Db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetAvailableProductsForSell()
        {
            return await SearchAsync(x => x.IsAvailableForSell == true);
        }

        public async Task<IEnumerable<Product>> GetProductsByUserIdAsync(Guid userId)
        {
            return await SearchAsync(x => x.OwnerId == userId);
        }

        public async Task<Product> GetProductExtractAsync(Guid productId) 
        {
            return await Db.Products.AsNoTracking().Include(x => x.Transactions).FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
