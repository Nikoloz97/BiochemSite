using System.ComponentModel.DataAnnotations;

namespace BiochemSite.Models.Content
{
    public class SubchapterForCreationDto
    {
        [Required]
        public int Number { get; set; }

        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(50)]
        public List<List<string>> Paragraphs { get; set; } = new List<List<string>>();
    }
}
