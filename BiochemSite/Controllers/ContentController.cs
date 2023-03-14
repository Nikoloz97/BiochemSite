using BiochemSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        // Use: All content
        [HttpGet]
        public ActionResult<IEnumerable<ContentDto>> GetAllContent()
        {
            var allContent = ContentDataStore.Instance.Contents;
            return Ok(allContent);
        }

        // Use: Content for a chapter
        [HttpGet("{chapterNum}")]
        public ActionResult<IEnumerable<ContentDto>> GetChapterContent(int chapterNum)
        {
            var chapterContent = ContentDataStore.Instance.Contents.Where(c => c.ChapterNum == chapterNum);

            return Ok(chapterContent);
        }

        // Use: Content for a subchapter
        [HttpGet("{chapterNum}/{subChapterNum}")]
        public ActionResult<ContentDto> GetSubchapterContent(int chapterNum, int subchapterNum)
        {
            var subchapterContent = ContentDataStore.Instance.Contents.FirstOrDefault(c => (c.ChapterNum == chapterNum) && (c.SubChapterNum == subchapterNum));
            if (subchapterContent == null)
            {
                return NotFound();
            }

            return Ok(subchapterContent);
        }
    }
}
