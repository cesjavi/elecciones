using MongoDB.Driver;
using BCrypt.Net;

namespace Elecciones.Services
{ 
    public class AuthService
    {
        private readonly IMongoCollection<User> _users;

        public AuthService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = "mongodb://localhost:27017";
            }

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("elecciones");
            _users = database.GetCollection<User>("users");
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<bool> RegisterAsync(string username, string password, string dni)
        {
            var existing = await GetUserAsync(username);
            if (existing != null)
                return false;

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Dni = dni
            };
            await _users.InsertOneAsync(user);
            return true;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await GetUserAsync(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user;
            return null;
        }
    }

}