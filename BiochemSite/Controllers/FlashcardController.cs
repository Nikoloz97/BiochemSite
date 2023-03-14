using BiochemSite.Models;
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

        // Use: Decks for specific chapter
        [HttpGet("{chapterNum}")]
        public ActionResult<IEnumerable<FlashcardDto>> GetChapterDecks (int chapterNum)
        {
            var chapterDecks = FlashcardDataStore.Instance.Flashcards.Where(fc => fc.ChapterNum == chapterNum);
 
            return Ok(chapterDecks);
        }

        // Use: Deck for a subchapter
        [HttpGet("{chapterNum}/{subchapterNum}")]
        public ActionResult<FlashcardDto> GetSubchapterDeck(int chapterNum, int subchapterNum)
        {
            var subchapterDeck = FlashcardDataStore.Instance.Flashcards.Where(fc => (fc.ChapterNum== chapterNum) && (fc.SubchapterNum == subchapterNum));

            if (subchapterDeck == null)
            {
                return NotFound();
            }

            return Ok(subchapterDeck);
        }




    }
}
