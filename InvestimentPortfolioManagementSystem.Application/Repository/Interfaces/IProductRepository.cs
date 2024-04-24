using InvestimentPortfolioManagementSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Product>> GetAvailableProductsForSell();
        Task<IEnumerable<Product>> GetProductsByUserIdAsync(Guid userId);
        Task<Product> GetProductExtractAsync(Guid productId);
    }
}
