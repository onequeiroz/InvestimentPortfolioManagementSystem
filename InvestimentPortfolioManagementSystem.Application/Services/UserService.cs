using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Models.Enums;
using InvestimentPortfolioManagementSystem.Application.Repository;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public UserService(IUserRepository userRepository, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<int> AddAsync(User userModel)
        {
            if (!Enum.IsDefined(typeof(UserTypeEnum), userModel.UserType))
            {
                throw new Exception("User type does not exist.");
            }

            if (_userRepository.SearchAsync(x => x.Name == userModel.Name).Result.Any())
            {
                throw new Exception($"User {userModel.Name} already exists.");
            }

            if (_userRepository.SearchAsync(x => x.EmailAddress == userModel.EmailAddress).Result.Any())
            {
                throw new Exception($"Email {userModel.EmailAddress} already exists.");
            }

            return await _userRepository.AddAsync(userModel);
        }
        
        public async Task<int> UpdateAsync(User userModel)
        {
            User user = await _userRepository.GetUserByIdAsync(userModel.Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (!Enum.IsDefined(typeof(UserTypeEnum), userModel.UserType))
            {
                throw new Exception("User type does not exist.");
            }

            if (userModel.Name != user.Name && _userRepository.SearchAsync(x => x.Name == userModel.Name).Result.Any())
            {
                throw new Exception($"User {userModel.Name} already exists.");
            }

            if (userModel.EmailAddress != user.EmailAddress && _userRepository.SearchAsync(x => x.EmailAddress == userModel.EmailAddress).Result.Any())
            {
                throw new Exception($"Email {userModel.EmailAddress} already exists.");
            }

            user.Name = userModel.Name;
            user.EmailAddress = userModel.EmailAddress;
            user.UserType = userModel.UserType;
            user.IsActive = userModel.IsActive;

            return await _userRepository.UpdateAsync(userModel);
        }

        public async Task<int> EnableDisableUserAsync(User user, bool activate = true)
        {
            if (activate)
            {
                if (user.IsActive)
                {
                    throw new Exception("User is already Active");
                }

                user.IsActive = true;
            }
            else
            {
                if (!user.IsActive)
                {
                    throw new Exception("User is already Inactive");
                }

                user.IsActive = false;
            }


            return await _userRepository.UpdateAsync(user);
        }

        public async Task<int> RemoveAsync(Guid userId)
        {
            IEnumerable<Product> userProducts = await _productRepository.GetProductsByUserIdAsync(userId);
            if (userProducts.Any())
            {
                throw new Exception("User owns products and cannot be deleted. Instead, try disabling the User.");
            }

            return await _userRepository.RemoveAsync(userId);
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
