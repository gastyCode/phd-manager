namespace PhDManager.Core.Models
{
    public class Thesis
    {
        public int ThesisId { get; set; }
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? StudentId { get; set; }
        public int SupervisorId { get; set; }
        public int? OpponentId { get; set; }
    }
}
