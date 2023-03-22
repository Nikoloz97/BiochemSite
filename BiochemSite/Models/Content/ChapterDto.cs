namespace BiochemSite.Models.Content
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public List<SubchapterDto> Subchapters { get; set; } = new List<SubchapterDto>();
    }
}
