using BiochemSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusExamController : ControllerBase
    {
        // Use: all status questions
        [HttpGet]
        public ActionResult<IEnumerable<StatusQuestionDto>> GetAllSQs()
        {
            var allSQs = StatusQuestionStore.Instance.StatusQuestions;
            return Ok(allSQs);
        }

        // Use: status questions for a chapter
        [HttpGet("{ChapterNum}")]
        public ActionResult<IEnumerable<StatusQuestionDto>> GetChapterSQs(int chapterNum)
        {
            var chapterSQs = StatusQuestionStore.Instance.StatusQuestions.Where(SQ => SQ.ChapterNum == chapterNum);
            return Ok(chapterSQs);
        }
     

        // Use: random status question for a chapter
        [HttpGet("Random/{ChapterNum}")]
        public ActionResult<StatusQuestionDto> GetRandomChapterSQ(int chapterNum)
        {
            var chapterSQs = StatusQuestionStore.Instance.StatusQuestions.Where(SQ => SQ.ChapterNum == chapterNum).ToList();
            var random = new Random();
            int randomIndex = random.Next(chapterSQs.Count);
            var chapterSQ = chapterSQs[randomIndex];

            if (chapterSQ == null)
            {
                return NotFound();
            }
            return Ok(chapterSQ);
        }

        // Use: status questions for a subchapter
        [HttpGet("{ChapterNum}/{SubchapterNum}")]
        public ActionResult<IEnumerable<StatusQuestionDto>> GetSubchapterSQs(int chapterNum, int subChapterNum)
        {
            var subchapterSQs = StatusQuestionStore.Instance.StatusQuestions.Where(SQ => SQ.ChapterNum == chapterNum && SQ.SubchapterNum == subChapterNum);
            return Ok(subchapterSQs);
        }

        // TODO: Test this function on Postman
        // Use: random status question for a subchapter
        [HttpGet("Random/{ChapterNum}/{SubchapterNum}")]
        public ActionResult<StatusQuestionDto> GetRandomSubchapterSQ(int chapterNum, int subChapterNum)
        {
            var subchapterSQs = StatusQuestionStore.Instance.StatusQuestions.Where(SQ => SQ.ChapterNum == chapterNum && SQ.SubchapterNum == subChapterNum).ToList();
            var random = new Random();
            int randomIndex = random.Next(subchapterSQs.Count);
            var subchapterSQ = subchapterSQs[randomIndex];

            if (subchapterSQ == null)
            {
                return NotFound();
            }
            return Ok(subchapterSQ);
        }

    }
}
