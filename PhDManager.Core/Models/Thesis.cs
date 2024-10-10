using System.ComponentModel.DataAnnotations;

namespace PhDManager.Core.Models
{
    public class Thesis
    {
        public Guid ThesisId { get; set; }
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public User? Student { get; set; }
        public User Supervisor { get; set; } = null!;
    }
}
