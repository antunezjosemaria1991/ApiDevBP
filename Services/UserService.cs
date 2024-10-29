using ApiDevBP.Configurations;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ApiDevBP.Services
{
	public class UserService : IUserService
	{
		private readonly DatabaseHandler _dbHandler;
		private readonly ILogger<UserService> _logger;

		public UserService(DatabaseHandler dbHandler, ILogger<UserService> logger)
		{
			_dbHandler = dbHandler;
			_logger = logger;
		}

		public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
		{
			try
			{
				var users = _dbHandler.GetUsers();
				return users.Select(user => new UserModel
				{
					Name = user.Name,
					Lastname = user.Lastname
				}).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving users.");
				throw;
			}
		}

		public async Task<UserModel> GetUserByIdAsync(int id)
		{
			try
			{
				var user = _dbHandler.GetUserById(id);
				if (user == null) return null;

				return new UserModel
				{
					Name = user.Name,
					Lastname = user.Lastname
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving user by ID.");
				throw;
			}
		}

		public async Task<bool> CreateUserAsync(UserModel userModel)
		{
			try
			{
				var userEntity = new UserEntity
				{
					Name = userModel.Name,
					Lastname = userModel.Lastname
				};
				_dbHandler.AddUser(userEntity);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating user.");
				return false;
			}
		}

		public async Task<bool> UpdateUserAsync(int id, UserModel userModel)
		{
			try
			{
				var user = _dbHandler.GetUserById(id);
				if (user == null) return false;

				user.Name = userModel.Name;
				user.Lastname = userModel.Lastname;
				_dbHandler.UpdateUser(user);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating user.");
				return false;
			}
		}

		public async Task<bool> DeleteUserAsync(int id)
		{
			try
			{
				var user = _dbHandler.GetUserById(id);
				if (user == null) return false;

				_dbHandler.DeleteUser(id);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting user.");
				return false;
			}
		}
	}
}
