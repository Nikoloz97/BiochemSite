using BiochemSite.Models;

namespace BiochemSite
{
    public class StatusQuestionStore
    {
        public List<StatusQuestionDto> StatusQuestions { get; set; }

        public static StatusQuestionStore Instance { get; }  = new StatusQuestionStore();

        public StatusQuestionStore()
        {
            StatusQuestions = new List<StatusQuestionDto>()
            {
                new StatusQuestionDto() {Id = 1, ChapterNum = 1, SubchapterNum = 1, Prompt = "What is this amino acid?", Explanation = "Lysine is a base that contains and R-group that consists of an amine followed by four carbons"},
                new StatusQuestionDto() {Id = 2, ChapterNum = 1, SubchapterNum = 2, Prompt = "What is a pka", Explanation = "It is the tendency for a functional group to dissociate"},
                new StatusQuestionDto() {Id = 3, ChapterNum = 2, SubchapterNum = 1, Prompt = "What is a base", Explanation = "Something that wants to be protonated"},
            };
        }


    }
}
