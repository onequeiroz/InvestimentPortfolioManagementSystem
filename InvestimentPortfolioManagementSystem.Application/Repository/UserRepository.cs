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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await Db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
