using BiochemSite.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        // Get all content
        [HttpGet]
        public ActionResult<IEnumerable<ContentDto>> GetAllContent()
        {
            var allContent = ContentDataStore.Instance.Contents;
            return Ok(allContent);
        }

        // Get content for a chapter
        [HttpGet("{chapterNum}")]
        public ActionResult<IEnumerable<ContentDto>> GetChapterContent(int chapterNum)
        {
            var chapterContent = ContentDataStore.Instance.Contents.Where(c => c.ChapterNum == chapterNum);

            return Ok(chapterContent);
        }

        // Content for a subchapter
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


        // Add a chapter (admin)
        [HttpPost("{chapterNum}")]
        public ActionResult<ContentDto> CreateChapterContent (int chapterNum, ContentForCreationDto chapterToAdd)
        {
            var maxContentId = ContentDataStore.Instance.Contents.Max(c => c.Id);


            var newChapter = new ContentDto()
            {
                Id = ++maxContentId,
                ChapterNum = chapterToAdd.ChapterNum,
                SubChapterNum = chapterToAdd.SubChapterNum
            };

            ContentDataStore.Instance.Contents.Add(newChapter);

            return Ok();
        }







        // Add a subchapter (admin)
        [HttpPost("{chapterNum}/{subChapterNum}")]
        public ActionResult<ContentDto> CreateSubchapterContent(int chapterNum, int subchapterNum)
        {
            return Ok();
        }

        // Editing a chapter (admin)
        [HttpPatch("{chapterNum}")]
        public ActionResult<ContentDto> EditChapterContent(int chapterNum)
        {
            return Ok();
        }

        // Editing a subchapter (admin)
        [HttpPatch("{chapterNum}/{subChapterNum}")]
        public ActionResult<ContentDto> EditSubchapterContent(int chapterNum, int subchapterNum)
        {
            return Ok();
        }

        // Deleting a chapter (admin) 
        [HttpDelete("{chapterNum}")]
        public ActionResult<ContentDto> DeleteSubchapterContent(int chapterNum)
        {
            return Ok();
        }

        // Deleting a subchapter (admin) 
        [HttpDelete("{chapterNum}/{subChapterNum}")]
        public ActionResult<ContentDto> DeleteSubchapterContent(int chapterNum, int subchapterNum)
        {
            return Ok();
        }

    }
}
