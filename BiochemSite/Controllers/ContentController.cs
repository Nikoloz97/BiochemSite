using BiochemSite.DataStores;
using BiochemSite.Models.Content;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly ILogger<ContentController> _logger;

        public ContentController(ILogger<ContentController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }





        // Get all content
        [HttpGet]
        public ActionResult<IEnumerable<ChapterDto>> GetAllContent()
        {
            var allContent = ContentDataStore.Instance.Contents;
            return Ok(allContent);
        }

        // Get content for a chapter
        [HttpGet("{chapterNum}", Name = "GetChapter")]
        public ActionResult<IEnumerable<ChapterDto>> GetChapterContent(int chapterNum)
        {
            var chapterContent = ContentDataStore.Instance.Contents.FirstOrDefault(c => c.Number == chapterNum);

            if (chapterContent == null)
            {
                _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing chapters");
                return NotFound();
            }

            return Ok(chapterContent);
        }

        // Content for a subchapter
        [HttpGet("{chapterNum}/{subChapterNum}", Name = "GetSubchapter")]
        public ActionResult<ChapterDto> GetSubchapterContent(int chapterNum, int subchapterNum)
        {
            var correctChapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => (c.Number == chapterNum));

            if (correctChapter == null)
            {
                _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing chapters");
                return NotFound();
            }

            var subchapterContent = correctChapter.Subchapters.FirstOrDefault(sc => (sc.Number == subchapterNum));

            if (subchapterContent == null)
            {
                _logger.LogInformation($"Subchapter number {subchapterNum} wasn't found when accessing subchapters for Chapter number {chapterNum}");
                return NotFound();
            }

            return Ok(subchapterContent);
        }


        // Create a chapter (admin)
        [HttpPost]
        public ActionResult<ChapterDto> CreateChapterContent (ChapterForCreationDto chapterToAdd)
        {
            var maxChapterId = ContentDataStore.Instance.Contents.Max(c => c.Id);

            var newChapter = new ChapterDto()
            {
                Id = ++maxChapterId,
                Number = chapterToAdd.Number,
                Title = chapterToAdd.Title,
                Subchapters = chapterToAdd.Subchapters,
            };

            ContentDataStore.Instance.Contents.Add(newChapter);

            return CreatedAtRoute("GetChapter",
                new
                {
                    chapterNum = chapterToAdd.Number,
                },
                newChapter
                );
        }


        // Create a subchapter (admin)
        [HttpPost("{chapterNum}", Name = "CreateSubchapter")]
        public ActionResult<SubchapterDto> CreateSubchapterContent(int chapterNum, SubchapterForCreationDto chapterToAdd)
        {
            var CorrespondingChapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => (c.Number == chapterNum));

            if (CorrespondingChapter == null)
            {
                _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing subchapters");
                return NotFound();
            }

            var maxSubchapterId = CorrespondingChapter.Subchapters.Max(sc => sc.Id);

            var newSubchapter = new SubchapterDto()
            {
                Id = ++maxSubchapterId,
                Number = chapterToAdd.Number,
                Title = chapterToAdd.Title,
                Paragraphs = chapterToAdd.Paragraphs,
            };

            CorrespondingChapter.Subchapters.Add(newSubchapter);

            // Referring to correpsonding get request's: name, parameters, and mapped resource 
            return CreatedAtRoute("GetSubchapter",
                new
                {
                    chapterNum = chapterNum,
                    subchapterNum = chapterToAdd.Number,
                },
                newSubchapter
                );
        }

        // Edit a chapter (admin)
        [HttpPut("{chapterNum}", Name = "EditChapter")]
        public ActionResult UpdateChapterContent(int chapterNum, ChapterForUpdateDto updatedChapter)
        {
            var chapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => c.Number == chapterNum);

            if (chapter == null)
            {
                _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing subchapters");
                return NotFound();
            }

            chapter.Number = updatedChapter.Number;
            chapter.Title = updatedChapter.Title;
            chapter.Subchapters = updatedChapter.Subchapters;

            return NoContent();
        }

        // Edit a subchapter (admin)
        [HttpPut("{chapterNum}/{subchapterNum}", Name = "EditSubchapter")]
        public ActionResult UpdateSubchapterContent(int chapterNum, int subchapterNum, SubchapterForUpdateDto updatedChapter)
        {
            var correspondingChapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => (c.Number == chapterNum));

            if (correspondingChapter == null)
            {
                _logger.LogInformation($"Chapter number {subchapterNum} wasn't found when accessing subchapters");
                return NotFound();
            }

            var subchapter = correspondingChapter.Subchapters.FirstOrDefault(sc => (sc.Number == subchapterNum));

            if (subchapter == null)
            {
                _logger.LogInformation($"Subchapter number {subchapterNum} wasn't found when accessing subchapters for Chapter number {chapterNum}");
                return NotFound();
            }

            subchapter.Number = updatedChapter.Number;
            subchapter.Title = updatedChapter.Title;
            subchapter.Paragraphs = updatedChapter.Paragraphs;

            return NoContent();
        }






        // Partially edit a chapter (admin)
        [HttpPatch("{chapterNum}", Name = "PartiallyEditChapter")]
        public ActionResult<ChapterDto> EditChapterContent(int chapterNum, JsonPatchDocument<ChapterForUpdateDto> patchDocument)
        {
            // 1. Find the chapter
            var chapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => c.Number == chapterNum);

            if (chapter == null)
            {
                _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing subchapters for Chapter");
                return NotFound();
            }

            // 2. Map chapter properties -> chapterForUpdateDto
            var chapterToPatch =
               new ChapterForUpdateDto()
               {
                   Number = chapter.Number,
                   Title = chapter.Title,
                   Subchapters = chapter.Subchapters,
               };

            // 3. Apply that mapped variable to patchdoc (see body of patch request in postman -> "path" value = matches for property"
                    // ModelState = allows to return message in case something goes wrong
            patchDocument.ApplyTo(chapterToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 4. Change chapter variable's properties from store with the patched version
            chapter.Title = chapterToPatch.Title;
            chapter.Number = chapterToPatch.Number;
            chapter.Subchapters = chapterToPatch.Subchapters;

            return NoContent();
        }

        // Partially edit a subchapter (admin)
        [HttpPatch("{chapterNum}/{subChapterNum}", Name ="PartiallyEditSubchapter")]
        public ActionResult<ChapterDto> EditSubchapterContent(int chapterNum, int subchapterNum, JsonPatchDocument<SubchapterForUpdateDto> patchDocument)
        {
            var correspondingChapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => c.Number == chapterNum);
            if (correspondingChapter == null)
            {
                _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing subchapters");
                return NotFound();
            }

            var subchapter = correspondingChapter.Subchapters.FirstOrDefault(c => c.Number == subchapterNum);
            if (subchapter == null)
            {
                _logger.LogInformation($"Subchapter number {subchapterNum} wasn't found when accessing subchapters for Chapter number {chapterNum}");
                return NotFound();
            }

            var subchapterToPatch =
                new SubchapterForUpdateDto()
                {
                    Number = subchapter.Number,
                    Title = subchapter.Title,
                    Paragraphs = subchapter.Paragraphs,
                };

            patchDocument.ApplyTo(subchapterToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            subchapter.Title  = subchapterToPatch.Title;
            subchapter.Number = subchapterToPatch.Number;
            subchapter.Paragraphs = subchapterToPatch.Paragraphs;

            return NoContent();
        }









        // Delete a chapter (admin) 
        [HttpDelete("{chapterNum}", Name = "DeleteChapter")]
        public ActionResult<ChapterDto> DeleteSubchapterContent(int chapterNum)
        {
            var chapter = ContentDataStore.Instance.Contents.FirstOrDefault(chap => chap.Number == chapterNum);
            if (chapter == null)
            {
                _logger.LogInformation($"Chapter number  {chapterNum}  wasn't found when accessing subchapters");
                return NotFound();
            }

            ContentDataStore.Instance.Contents.Remove(chapter);

            return NoContent();
        }

        // Delete a subchapter (admin) 
        [HttpDelete("{chapterNum}/{subChapterNum}", Name = "DeleteSubchapter")]
        public ActionResult<ChapterDto> DeleteSubchapterContent(int chapterNum, int subchapterNum)
        {
            var chapter = ContentDataStore.Instance.Contents.FirstOrDefault(chap => chap.Number == chapterNum);
            if (chapter == null)
            {
                _logger.LogInformation($"Chapter number  {chapterNum}  wasn't found when accessing subchapters");
                return NotFound();
            }

            var subchapter = chapter.Subchapters.FirstOrDefault(subchap => subchap.Number == subchapterNum);

            if (subchapter == null) 
            {
                _logger.LogInformation($"Subchapter number {subchapterNum} wasn't found when accessing subchapters for Chapter number {chapterNum}");
                return NotFound(); 
            }

            chapter.Subchapters.Remove(subchapter);
            
            return NoContent();
        }

    }
}
