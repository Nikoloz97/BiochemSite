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
        [HttpGet("{chapterNum}", Name = "GetChapter")]
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


        // Create a chapter (admin)
        [HttpPost]
        public ActionResult<ContentDto> CreateChapterContent (ContentForCreationDto chapterToAdd)
        {
            var maxContentId = ContentDataStore.Instance.Contents.Max(c => c.Id);


            var newChapter = new ContentDto()
            {
                Id = ++maxContentId,
                ChapterNum = chapterToAdd.ChapterNum,
                SubChapterNum = chapterToAdd.SubChapterNum,
                ChapDesc = chapterToAdd.ChapDesc
            };

            ContentDataStore.Instance.Contents.Add(newChapter);

            return CreatedAtRoute("GetChapter",
                new
                {
                    chapterNum = chapterToAdd.ChapterNum,
                },
                newChapter
                );
        }


        // Create a subchapter (admin)
        [HttpPost("{chapterNum}")]
        public ActionResult<ContentDto> CreateSubchapterContent(int chapterNum, ContentForCreationDto chapterToAdd)
        {
            return Ok();
        }




        // Edit a chapter (admin)
        [HttpPatch("{chapterNum}")]
        public ActionResult<ContentDto> EditChapterContent(int chapterNum)
        {
            return Ok();
        }

        // Edit a subchapter (admin)
        [HttpPatch("{chapterNum}/{subChapterNum}")]
        public ActionResult<ContentDto> EditSubchapterContent(int chapterNum, int subchapterNum)
        {
            return Ok();
        }

        // Delete a chapter (admin) 
        [HttpDelete("{chapterNum}")]
        public ActionResult<ContentDto> DeleteSubchapterContent(int chapterNum)
        {
            return Ok();
        }

        // Delete a subchapter (admin) 
        [HttpDelete("{chapterNum}/{subChapterNum}")]
        public ActionResult<ContentDto> DeleteSubchapterContent(int chapterNum, int subchapterNum)
        {
            return Ok();
        }

    }
}
