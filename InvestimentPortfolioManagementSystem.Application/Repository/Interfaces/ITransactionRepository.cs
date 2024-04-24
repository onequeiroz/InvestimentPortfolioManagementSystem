using InvestimentPortfolioManagementSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Repository.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction> GetTransactionByIdAsync(Guid transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId);
        Task<IEnumerable<Transaction>> GetTransactionsByProductIdAsync(Guid productId);
    }
}
