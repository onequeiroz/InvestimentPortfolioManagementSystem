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
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context) { }

        public async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
        {
            return await Db.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId)
        {
            return await Db.Transactions.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByProductIdAsync(Guid productId)
        {
            return await Db.Transactions.AsNoTracking().Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
