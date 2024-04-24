using InvestimentPortfolioManagementSystem.Application.Models;

namespace InvestimentPortfolioManagementSystem.Application.Services.Interfaces
{
    public interface ITransactionService : IDisposable
    {
        Task<int> AddAsync(Transaction transactionModel);
        Task<int> RemoveAsync(Guid id);
    }
}