using System.ComponentModel.DataAnnotations;

namespace PhDManager.Core.ValidationModels
{
    public class ThesisData
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
    }
}
