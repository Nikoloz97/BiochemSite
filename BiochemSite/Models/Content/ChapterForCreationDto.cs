using System.ComponentModel.DataAnnotations;

namespace BiochemSite.Models.Content
{
    
    public class ChapterForCreationDto
    {
        [Required]
        public int Number { get; set; }

        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(50)]
        public List<SubchapterDto> Subchapters { get; set; } = new List<SubchapterDto>();

    }
}
