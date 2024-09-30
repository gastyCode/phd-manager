namespace PhDManager.Core.Models
{
    public class AuthResponse
    {
        public User User { get; set; } = default!;
        public string Token { get; set; } = string.Empty;
    }
}
