using Bearer_TokenJWT_balta.io.Models;

namespace Bearer_TokenJWT_balta.io.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new() { Id = 1, Username = "batman", Password = "batman", Role = "manager" },
                new() { Id = 2, Username = "robin", Password = "robin", Role = "employee" }
            };

            return users
                .FirstOrDefault(u =>
                    u.Username == username
                    && u.Password == password);
        }
    }
}
