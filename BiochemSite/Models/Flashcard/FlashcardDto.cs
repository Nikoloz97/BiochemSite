namespace BiochemSite.Models.Flashcard
{
    public class FlashcardDto
    {
        public int Id { get; set; }
        public int ChapterNum { get; set; }
        public int SubchapterNum { get; set; }
        public string? ImageURL { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public int ConfidenceLevel { get; set; }


    }
}
