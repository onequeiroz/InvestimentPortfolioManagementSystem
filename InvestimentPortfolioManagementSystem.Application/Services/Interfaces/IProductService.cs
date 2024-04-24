using InvestimentPortfolioManagementSystem.Application.Models;

namespace InvestimentPortfolioManagementSystem.Application.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task<int> AddAsync(ProductRequest productModel);
        Task<int> UpdateAsync(ProductRequest productModel);
        Task<int> EnableDisableProductForSellAsync(Product product, bool enableForSell = true);
        Task<int> RemoveAsync(Guid id);
    }
}