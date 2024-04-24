using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;

namespace InvestimentPortfolioManagementSystem.Application.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<int> AddAsync(User userModel);
        Task<int> UpdateAsync(User userModel);
        Task<int> EnableDisableUserAsync(User user, bool activate = true);
        Task<int> RemoveAsync(Guid id);
    }
}