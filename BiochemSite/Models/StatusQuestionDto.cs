namespace BiochemSite.Models
{
    public class StatusQuestionDto
    {
        public int Id { get; set; }
        public int ChapterNum { get; set; }
        public int SubchapterNum { get; set; }
        public string Prompt { get; set; } = string.Empty;

        public string OptionOne { get; set; } = string.Empty;
        public string OptionTwo { get; set; } = string.Empty;
        public string OptionThree { get; set; } = string.Empty;
        public string OptionFour { get; set; } = string.Empty;

        public string CorrectOption { get; set; } = string.Empty;

        public ExplanationDto Explanation { get; set; } = new ExplanationDto();

        public string? ImageURL { get; set; }    

    }
}
