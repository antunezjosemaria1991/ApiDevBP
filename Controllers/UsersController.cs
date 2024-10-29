using ApiDevBP.Models;
using ApiDevBP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevBP.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly ILogger<UsersController> _logger;

		public UsersController(IUserService userService, ILogger<UsersController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		// Get all users (GET)
		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _userService.GetAllUsersAsync();
			if (users == null || !users.Any())
			{
				return NotFound("No users found.");
			}
			return Ok(users);
		}

		// Get a single user by ID (GET)
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound("User not found.");
			}
			return Ok(user);
		}

		// Create a new user (POST)
		[HttpPost]
		public async Task<IActionResult> SaveUser(UserModel userModel)
		{
			var result = await _userService.CreateUserAsync(userModel);
			if (!result)
			{
				return StatusCode(500, "An error occurred while creating the user.");
			}
			return Ok("User created successfully.");
		}

		// Update a user (PUT)
		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateUser(int id, UserModel userModel)
		{
			var result = await _userService.UpdateUserAsync(id, userModel);
			if (!result)
			{
				return StatusCode(500, "An error occurred while updating the user.");
			}
			return Ok("User updated successfully.");
		}

		// Delete a user (DELETE)
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var result = await _userService.DeleteUserAsync(id);
			if (!result)
			{
				return StatusCode(500, "An error occurred while deleting the user.");
			}
			return Ok("User deleted successfully.");
		}
	}
}
