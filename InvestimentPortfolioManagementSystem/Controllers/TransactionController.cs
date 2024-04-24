using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Repository;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestimentPortfolioManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost(Name = "Create Transaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transactionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int updatedRows = await _transactionService.AddAsync(transactionModel);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Insertion failed. Please talk to the Administrator.");
        }
    }
}
