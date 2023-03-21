using BiochemSite.Models.Flashcard;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite
{
    public class FlashcardDataStore 
    {
        public List<FlashcardDto> Flashcards { get; set; }  
        public static FlashcardDataStore Instance { get; } = new FlashcardDataStore();
        public FlashcardDataStore()
        {
            Flashcards = new List<FlashcardDto>()
            {
                new FlashcardDto() { Id = 1, ChapterNum = 1, SubchapterNum = 1, ImageURL = "https://microbenotes.com/wp-content/uploads/2021/07/Amino-Acids.jpeg", Prompt = "The parts of the amino acids are the amino group, hydrogen, carboxyl, and r-group", ConfidenceLevel = 0},
                 new FlashcardDto() { Id = 2, ChapterNum = 1, SubchapterNum = 2, ImageURL = "https://o.quizlet.com/sCIsgxuGPtopiHeMa8d3mg.png", Prompt = "Typically, the pka of carboxyl group is around 2-4, while that of amino group is around 9-10", ConfidenceLevel = 0},
                  new FlashcardDto() { Id = 3, ChapterNum = 2, SubchapterNum = 1, ImageURL = "https://www.mun.ca/biology/scarr/iGen3_06-03_Figure-Lsmc.jpg", Prompt = "Peptide bonds are formed when the amino group backside-attacks the carboxyl group, forming a type of amide bond", ConfidenceLevel = 0}

            };

        }
    }
}
