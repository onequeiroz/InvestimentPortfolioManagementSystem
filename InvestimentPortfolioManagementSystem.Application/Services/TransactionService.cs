using InvestimentPortfolioManagementSystem.Application.Models.Enums;
using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public TransactionService(ITransactionRepository transactionRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<int> AddAsync(Transaction transactionModel)
        {
            if (!Enum.IsDefined(typeof(TransactionTypeEnum), transactionModel.TransactionType))
            {
                throw new Exception("Transaction type does not exist.");
            }

            User user = await _userRepository.GetUserByIdAsync(transactionModel.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!user.IsActive)
            {
                throw new Exception("Inactive User cannot Buy or Sell products!");
            }

            if (user.UserType != UserTypeEnum.User)
            {
                throw new Exception("Managers are not allowed to negociate products!");
            }

            Product product = await _productRepository.GetProductByIdAsync(transactionModel.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (!product.IsAvailableForSell)
            {
                throw new Exception("Unavailable products cannot be negociated!");
            }

            if (transactionModel.ProductValue == 0)
            {
                transactionModel.ProductValue = product.Value;
            }

            int rowsAffected = await _transactionRepository.AddAsync(transactionModel);

            if (rowsAffected > 0) 
            {
                if (transactionModel.TransactionType == TransactionTypeEnum.Buy)
                {
                    product.OwnerId = transactionModel.UserId;
                    return await _productRepository.UpdateAsync(product);
                }

                return rowsAffected;
            }

            return 0;
        }

        public async Task<int> RemoveAsync(Guid productId)
        {
            return await _transactionRepository.RemoveAsync(productId);
        }

        public void Dispose()
        {
            _transactionRepository?.Dispose();
        }
    }
}
