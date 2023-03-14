namespace BiochemSite.Models
{
    public class StatusQuestionDto
    {
        public int Id { get; set; }
        public int ChapterNum { get; set; }
        public int SubchapterNum { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public AnswerOptionsDto AnswerOptions { get; set; } = new AnswerOptionsDto();
        public string Explanation { get; set; } = string.Empty;
        public string? ImageURL { get; set; }    

    }
}
