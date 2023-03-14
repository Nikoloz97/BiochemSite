using BiochemSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusExamController : ControllerBase
    {
        [HttpGet]
        public ActionResult<StatusQuestionDto> GetAllSQs()
        {
            var allSQs = StatusQuestionStore.Instance.StatusQuestions;
            return Ok(allSQs);
        }

        [HttpGet("{ChapterNum}")]
        public ActionResult<StatusQuestionDto> GetChapterSQs(int chapterNum)
        {
            var chapterSQs = StatusQuestionStore.Instance.StatusQuestions.Where(SQ => SQ.ChapterNum == chapterNum);
            return Ok(chapterSQs);
        }

        [HttpGet("{ChapterNum}/{SubchapterNum]")]
        public ActionResult<StatusQuestionDto> GetSubchapterSQs(int chapterNum, int subChapterNum)
        {
            var subchapterSQs = StatusQuestionStore.Instance.StatusQuestions.Where(SQ => SQ.ChapterNum == chapterNum && SQ.SubchapterNum == subChapterNum);
            return Ok(subchapterSQs);
        }
    }
}
