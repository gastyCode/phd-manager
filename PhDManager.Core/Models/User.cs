namespace PhDManager.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Uid { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime? FirstLogin { get; set; }

        public List<Thesis> Theses { get; set; } = new List<Thesis>();
    }
}
