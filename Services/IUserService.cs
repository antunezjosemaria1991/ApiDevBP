using ApiDevBP.Models;

namespace ApiDevBP.Services
{
	public interface IUserService
	{
		Task<IEnumerable<UserModel>> GetAllUsersAsync();
		Task<UserModel> GetUserByIdAsync(int id);
		Task<bool> CreateUserAsync(UserModel userModel);
		Task<bool> UpdateUserAsync(int id, UserModel userModel);
		Task<bool> DeleteUserAsync(int id);
	}
}