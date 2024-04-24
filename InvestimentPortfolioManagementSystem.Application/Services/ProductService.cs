using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Models.Enums;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ProductService(IProductRepository productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<int> AddAsync(ProductRequest productModel)
        {
            User operationUser = await _userRepository.GetUserByIdAsync((Guid)productModel.UserOperationId);
            if (operationUser == null)
            {
                throw new Exception("Registration User not found.");
            }

            if (operationUser.UserType != UserTypeEnum.Manager)
            {
                throw new Exception("Only Managers are allowed to Create Products");
            }

            if (productModel.OwnerId != null)
            {
                throw new Exception("A new Product Owner can only be set in a negociation (Buy operation)");
            }

            if (_productRepository.SearchAsync(x => x.Name == productModel.Name).Result.Any())
            {
                throw new Exception($"Product {productModel.Name} already exists.");
            }

            return await _productRepository.AddAsync(productModel);
        }

        public async Task<int> UpdateAsync(ProductRequest productModel)
        {
            User operationUser = await _userRepository.GetUserByIdAsync((Guid)productModel.UserOperationId);
            if (operationUser == null)
            {
                throw new Exception("Operation User not found.");
            }

            if (operationUser.UserType != UserTypeEnum.Manager)
            {
                throw new Exception("Only Managers are allowed to Update Products");
            }

            Product product = await _productRepository.GetProductByIdAsync(productModel.Id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (productModel.OwnerId != product.OwnerId)
            {
                throw new Exception("A new Product Owner can only be set in a negociation (Buy operation)");
            }

            if (productModel.Name != product.Name && _productRepository.SearchAsync(x => x.Name == productModel.Name).Result.Any())
            {
                throw new Exception($"Product {productModel.Name} already exists.");
            }

            product.Name = productModel.Name;
            product.Description = productModel.Description;
            product.Value = productModel.Value;
            product.ExpirationDate = productModel.ExpirationDate;
            product.IsActive = productModel.IsActive;
            product.IsAvailableForSell = productModel.IsAvailableForSell;

            return await _productRepository.UpdateAsync(product);
        }

        public async Task<int> EnableDisableProductForSellAsync(Product product, bool enableForSell = true)
        {
            if (enableForSell)
            {
                if (product.IsAvailableForSell)
                {
                    throw new Exception("Product is already available for sell!");
                }

                product.IsAvailableForSell = true;
            }
            else
            {
                if (!product.IsAvailableForSell)
                {
                    throw new Exception("UserProduct is already unavailable for sell!");
                }

                product.IsAvailableForSell = false;
            }


            return await _productRepository.UpdateAsync(product);
        }

        public async Task<int> RemoveAsync(Guid productId)
        {
            return await _productRepository.RemoveAsync(productId);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
