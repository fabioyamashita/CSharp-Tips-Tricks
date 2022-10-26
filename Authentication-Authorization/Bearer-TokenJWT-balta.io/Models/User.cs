namespace Bearer_TokenJWT_balta.io.Models
{
    // Simple class for our example
    
    // In a real world cenario:
    // Password must be encrypted
    // There are multiple roles
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
