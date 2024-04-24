using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestimentPortfolioManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet(Name = "Get All Users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet(Name = "Get User By Id")]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] Guid userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost(Name = "Create User")]
        public async Task<IActionResult> CreateUser([FromBody] User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int updatedRows = await _userService.AddAsync(userModel);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Insertion failed. Please talk to the Administrator.");
        }

        [HttpPut(Name = "Update User")]
        public async Task<IActionResult> UpdateUser([FromBody] User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int updatedRows = await _userService.UpdateAsync(userModel);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Update failed. Please talk to the Administrator.");
        }

        [HttpPut(Name = "Activate User")]
        public async Task<IActionResult> EnableUserAsync([FromBody] Guid userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            int updatedRows = await _userService.EnableDisableUserAsync(user);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Request failed. Please talk to the Administrator.");
        }

        [HttpPut(Name = "Inactivate User")]
        public async Task<IActionResult> DisableUserAsync([FromBody] Guid userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            int updatedRows = await _userService.EnableDisableUserAsync(user, false);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Request failed. Please talk to the Administrator.");
        }

        [HttpDelete(Name = "Delete User")]
        public async Task<IActionResult> DeleteUser([FromBody] Guid userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            int updatedRows = await _userService.RemoveAsync(userId);
            if (updatedRows > 0)
            {
                return Ok(updatedRows);
            }

            return BadRequest("Delete failed. Please talk to the Administrator.");
        }
    }
}
