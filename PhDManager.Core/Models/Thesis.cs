namespace PhDManager.Core.Models
{
    public class Thesis
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Student { get; set; } = string.Empty;
        public string Supervisor { get; set; } = string.Empty;
        public string Opponent { get; set; } = string.Empty;
    }
}
