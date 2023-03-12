using BiochemSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ContentDto> GetAllChapters()
        {
            var Chapters = ContentDataStore.Instance.Contents;
            return Ok(Chapters);
        }

        [HttpGet("{chapterNum}")]
        public ActionResult<ContentDto> GetSubChapters(int chapterNum)
        {
            var subChapters = ContentDataStore.Instance.Contents.Where(c => c.ChapterNum == chapterNum).ToList();
            if (subChapters.Count == 0)
            {
                return NotFound();
            }
            return Ok(subChapters);
        }

        


    }
}
