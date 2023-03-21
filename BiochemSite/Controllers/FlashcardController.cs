using BiochemSite.Models.Flashcard;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    // Setup: each subchapter contains an individual deck
    [ApiController]
    [Route("api/[controller]")]
    public class FlashCardController : Controller
    {
        // Use: All decks
        [HttpGet]
        public ActionResult<IEnumerable<FlashcardDto>> GetAllDecks()
        {
            var allDecks = FlashcardDataStore.Instance.Flashcards;

            return Ok(allDecks);
        }

        // Use: Deck for specific chapter
        [HttpGet("{chapterNum}")]
        public ActionResult<IEnumerable<FlashcardDto>> GetChapterDeck (int chapterNum)
        {
            var chapterDeck = FlashcardDataStore.Instance.Flashcards.Where(fc => fc.ChapterNum == chapterNum);
 
            return Ok(chapterDeck);
        }

        // Use: Deck for a subchapter
        [HttpGet("{chapterNum}/{subchapterNum}")]
        public ActionResult<IEnumerable<FlashcardDto>> GetSubchapterDeck(int chapterNum, int subchapterNum)
        {
            var subchapterDeck = FlashcardDataStore.Instance.Flashcards.Where(fc => (fc.ChapterNum== chapterNum) && (fc.SubchapterNum == subchapterNum));

            return Ok(subchapterDeck);
        }




    }
}
