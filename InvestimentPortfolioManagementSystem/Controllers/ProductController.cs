using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Repository;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Services;
using InvestimentPortfolioManagementSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestimentPortfolioManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductController(IProductRepository productRepository, IProductService productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }

        [HttpGet(Name = "Get All Products")]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet(Name = "Get Product By Id")]
        public async Task<IActionResult> GetProductByIdAsync([FromQuery] Guid productId)
        {
            Product product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet(Name = "Get Available Products For Sell")]
        public async Task<IActionResult> GetAvailableProductsForSell()
        {
            IEnumerable<Product> products = await _productRepository.GetAvailableProductsForSell();
            return Ok(products);
        }

        [HttpGet(Name = "Get Product Extract By Product Id")]
        public async Task<IActionResult> GetProductExtractByProductId([FromQuery] Guid productId)
        {
            Product product = await _productRepository.GetProductExtractAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet(Name = "Consult Products By Owner Id")]
        public async Task<IActionResult> ConsultProductsByOwnerId([FromQuery] Guid userId)
        {
            IEnumerable<Product> products = await _productRepository.GetProductsByUserIdAsync(userId);
            return Ok(products);
        }

        [HttpPost(Name = "Create Product")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int updatedRows = await _productService.AddAsync(productModel);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Insertion failed. Please talk to the Administrator.");
        }

        [HttpPut(Name = "Update Product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int updatedRows = await _productService.UpdateAsync(productModel);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Update failed. Please talk to the Administrator.");
        }

        [HttpPut(Name = "Enable Product For Sell")]
        public async Task<IActionResult> EnableProductForSellAsync([FromBody] Guid productId)
        {
            Product product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            int updatedRows = await _productService.EnableDisableProductForSellAsync(product);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Request failed. Please talk to the Administrator.");
        }

        [HttpPut(Name = "Disable Product For Sell")]
        public async Task<IActionResult> DisableProductForSellAsync([FromBody] Guid productId)
        {
            Product product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            int updatedRows = await _productService.EnableDisableProductForSellAsync(product, false);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Request failed. Please talk to the Administrator.");
        }

        [HttpDelete(Name = "Delete Product")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            Product product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            int updatedRows = await _productService.RemoveAsync(productId);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Delete failed. Please talk to the Administrator.");
        }
    }
}
