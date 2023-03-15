namespace BiochemSite.Models
{
    // TODO: change this to just "answer option" DTO
    // Contains option ID, option statement, isCorrectOption, Explanation
    public class AnswerOptionsDto
    {
        public string OptionOne { get; set; } = string.Empty;
        public string OptionTwo { get; set;} = string.Empty;
        public string OptionThree { get; set;} = string.Empty;
        public string OptionFour { get; set;} = string.Empty;

        public string CorrectOption { get; set;} = string.Empty;

        public string CorrectOptionExplanation { get; } = string.Empty; 
    }
}
