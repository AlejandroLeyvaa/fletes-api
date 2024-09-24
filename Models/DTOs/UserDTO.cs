namespace Fletes.Models.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }  // e.g., "Admin" or "User"
    }
}
