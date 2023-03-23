namespace BiochemSite.Models.Content
{
    public class SubchapterDto
    {
        public int Id { get; set; }
        public int Number { get; set; } 
        public string Title { get; set; } = string.Empty;
        public List<List<string>> Paragraphs { get; set; } = new List<List<string>>();
    }
}
