using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using InvestimentPortfolioManagementSystem.Application.Context;
using InvestimentPortfolioManagementSystem.Application.Services;
using InvestimentPortfolioManagementSystem.Application.Services.Interfaces;

namespace InvestimentPortfolioManagementSystem.API.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
