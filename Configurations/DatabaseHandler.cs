using ApiDevBP.Entities;
using Microsoft.Extensions.Options;
using SQLite;

namespace ApiDevBP.Configurations
{
	public class DatabaseHandler
	{
		private readonly SQLiteConnection _db;

		public DatabaseHandler(IOptions<DatabaseOptions> options)
		{
			var dbPath = options.Value.ConnectionString;

			// Inicializa la conexión SQLite con la cadena de conexión
			_db = new SQLiteConnection(dbPath);
			_db.CreateTable<UserEntity>(); // Crear tabla UserEntity si no existe
		}

		// Método para insertar un nuevo usuario
		public void AddUser(UserEntity user)
		{
			_db.Insert(user);
		}

		// Método para obtener todos los usuarios
		public List<UserEntity> GetUsers()
		{
			return _db.Table<UserEntity>().ToList();
		}

		// Método para obtener un usuario por ID
		public UserEntity GetUserById(int id)
		{
			return _db.Find<UserEntity>(id);
		}

		// Método para actualizar un usuario
		public void UpdateUser(UserEntity user)
		{
			_db.Update(user);
		}

		// Método para eliminar un usuario por ID
		public void DeleteUser(int id)
		{
			var user = _db.Find<UserEntity>(id);
			if (user != null)
			{
				_db.Delete(user);
			}
		}
	}
}