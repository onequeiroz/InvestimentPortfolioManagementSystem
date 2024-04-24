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
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionService transactionService, ITransactionRepository transactionRepository)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
        }

        [HttpGet(Name = "Get All Transactions")]
        public async Task<IActionResult> GetAllTransactionsAsync()
        {
            IEnumerable<Transaction> products = await _transactionRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet(Name = "Get Transaction By Id")]
        public async Task<IActionResult> GetTransactionByIdAsync([FromQuery] Guid transactionId)
        {
            Transaction transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpGet(Name = "Get Transactions By User Id")]
        public async Task<IActionResult> GetTransactionsByUserIdAsync([FromQuery] Guid userId)
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId);
            return Ok(transactions);
        }

        [HttpGet(Name = "Get Transactions By Product Id")]
        public async Task<IActionResult> GetTransactionsByProductIdAsync([FromQuery] Guid productId)
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetTransactionsByProductIdAsync(productId);
            return Ok(transactions);
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
