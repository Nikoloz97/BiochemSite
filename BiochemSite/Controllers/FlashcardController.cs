using BiochemSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlashCardController : Controller
    {
        // Use: get flashcards for entire chapter
        [HttpGet("{chapterNum}")]
        public ActionResult<FlashcardDto> GetChapterDeck(int chapterNum)
        {
            var deckToReturn = FlashcardDataStore.Instance.Flashcards.Where(fc => fc.ChapterNum== chapterNum).ToList();
            if (deckToReturn.Count == 0)
            {
                return NotFound();
            }
            return Ok(deckToReturn);
        }

        // Use: get flashcards for specific subchapter
        [HttpGet("{chapterNum}/{subchapterNum}")]
        public ActionResult<FlashcardDto> GetSubchapterDeck(int chapterNum, int subchapterNum)
        {
            var subchapterDeckToReturn = FlashcardDataStore.Instance.Flashcards.Where(fc => (fc.ChapterNum== chapterNum) && (fc.SubchapterNum == subchapterNum)).ToList();
            if (subchapterDeckToReturn.Count == 0)
            {
                return NotFound();
            }
            return Ok(subchapterDeckToReturn);
        }






    }
}
