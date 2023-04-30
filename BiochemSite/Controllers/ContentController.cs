using BiochemSite.DataStores;
using BiochemSite.Models.Content;
using BiochemSite.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly ILogger<ContentController> _logger;
        private readonly MailService _mailService;

        public ContentController(ILogger<ContentController> logger, MailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        // Get all content
        [HttpGet]
        public ActionResult<IEnumerable<ChapterDto>> GetAllContent()
        {
            try
            {
            var allContent = ContentDataStore.Instance.Contents;
            return Ok(allContent);

            }
            catch (Exception ex)
            {
                _logger.LogCritical("An exception occured when trying to get all chapter content", ex);
                return StatusCode(500, "An error occured when trying to process your request");
            }
        }

        // Get content for a chapter
        [HttpGet("{chapterNum}", Name = "GetChapter")]
        public ActionResult<IEnumerable<ChapterDto>> GetChapterContent(int chapterNum)
        {
            try
            {

                var chapterContent = ContentDataStore.Instance.Contents.FirstOrDefault(c => c.Number == chapterNum);

                if (chapterContent == null) 
                {
                    _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing chapters");
                    return NotFound();
                }

                return Ok(chapterContent);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured while getting chapter number {chapterNum}", ex);

                // Be vague here (don't want hackers to get any ideas...) 
                return StatusCode(500, "A problem occured while handling your request. Sorry!");
            }

           
        }

        // Content for a subchapter
        [HttpGet("{chapterNum}/{subChapterNum}", Name = "GetSubchapter")]
        public ActionResult<ChapterDto> GetSubchapterContent(int chapterNum, int subchapterNum)
        {

            try
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
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured while trying to get subchapter {subchapterNum} for chapter {chapterNum}", ex);

                return StatusCode(500, "A problem occured while handling your request. Sorry!");
            }

        }


        // Create a chapter (admin)
        [HttpPost]
        public ActionResult<ChapterDto> CreateChapterContent (ChapterForCreationDto chapterToAdd)
        {
            try
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

                _mailService.Send("Chapter Created", $"Chapter titled {chapterToAdd.Title} was created from the store with the following subchapters: {chapterToAdd.Subchapters}");


                // Referring to: get request's name, get request's parameters, and the mapped resource that was added
                return CreatedAtRoute("GetChapter",
                new
                {
                    chapterNum = chapterToAdd.Number,
                },
                newChapter
                );

            }

            catch (Exception ex)
            {
                _logger.LogCritical("Exception occured while trying to add a chapter", ex);

                return StatusCode(500, "A problem occured while handling your request");
            }
        }


        // Create a subchapter (admin)
        [HttpPost("{chapterNum}", Name = "CreateSubchapter")]
        public ActionResult<SubchapterDto> CreateSubchapterContent(int chapterNum, SubchapterForCreationDto subchapterToAdd)
        {
            try
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
                    Number = subchapterToAdd.Number,
                    Title = subchapterToAdd.Title,
                    Paragraphs = subchapterToAdd.Paragraphs,
                };

                CorrespondingChapter.Subchapters.Add(newSubchapter);

                _mailService.Send("Subchapter Created", $"Subchapter titled {subchapterToAdd.Title} was created for chapter {chapterNum}");

                // Referring to: get request's name, get request's parameters, and the mapped resource that was added
                return CreatedAtRoute("GetSubchapter",
                    new
                    {
                        chapterNum = chapterNum,
                        subchapterNum = subchapterToAdd.Number,
                    },
                    newSubchapter
                    );

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"A problem occured while creating a subchapter for chapter {chapterNum}", ex);

                return StatusCode(500, "A problem occured while handling your request");
            }

        }

        // Edit a chapter (admin)
        [HttpPut("{chapterNum}", Name = "EditChapter")]
        public ActionResult UpdateChapterContent(int chapterNum, ChapterForUpdateDto updatedChapter)
        {

            try
            {

                // Find chapter
                var chapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => c.Number == chapterNum);

                if (chapter == null)
                {
                    _logger.LogInformation($"Chapter number {chapterNum} wasn't found when accessing subchapters");
                    return NotFound();
                }

                // Map chapter
                chapter.Number = updatedChapter.Number;
                chapter.Title = updatedChapter.Title;
                chapter.Subchapters = updatedChapter.Subchapters;

                _mailService.Send("Chapter Edited", $"Chapter wih id {chapter.Id} was edited");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured while trying to edit chapter {chapterNum}", ex);
                return StatusCode(500, "An exception occured while handling your request");
            }
        }

        // Edit a subchapter (admin)
        [HttpPut("{chapterNum}/{subchapterNum}", Name = "EditSubchapter")]
        public ActionResult UpdateSubchapterContent(int chapterNum, int subchapterNum, SubchapterForUpdateDto updatedChapter)
        {

            try
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

                _mailService.Send("Subchapter Edited", $"Subchapter with id {subchapter.Id} was edited from Chapter {chapterNum}");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured while trying to edit subchapter {subchapterNum} for chapter {chapterNum}", ex);
                return StatusCode(500, "An error occured while handling your request");
            }


        }






        // Partially edit a chapter (admin)
        [HttpPatch("{chapterNum}", Name = "PartiallyEditChapter")]
        public ActionResult<ChapterDto> EditChapterContent(int chapterNum, JsonPatchDocument<ChapterForUpdateDto> patchDocument)
        {
            try
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

                // 3. Apply that mapped variable to patchdoc (see body of patch request on postman -> there, "path" = automatically matches for property"
                        // ModelState = returns message in case something goes wrong
                patchDocument.ApplyTo(chapterToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // 4. Change chapter variable's properties from store -> patched version
                chapter.Title = chapterToPatch.Title;
                chapter.Number = chapterToPatch.Number;
                chapter.Subchapters = chapterToPatch.Subchapters;

                _mailService.Send("Chapter Updated", $"Chapter id {chapter.Id} was partially edited");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured while trying to partially edit chapter {chapterNum}", ex);
                return StatusCode(500, "An exception occured while processing your request");
            }

        }

        // Partially edit a subchapter (admin)
        [HttpPatch("{chapterNum}/{subChapterNum}", Name ="PartiallyEditSubchapter")]
        public ActionResult<ChapterDto> EditSubchapterContent(int chapterNum, int subchapterNum, JsonPatchDocument<SubchapterForUpdateDto> patchDocument)
        {
            try
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

                _mailService.Send("Subchapter Edited", $"Subchapter id {subchapter.Id} from chapter {chapterNum} was partially edited");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured while trying to partially edit subchapter {subchapterNum} for chapter {chapterNum}", ex);
                return StatusCode(500, "An error occured while trying to process your request");
            }

        }









        // Delete a chapter (admin) 
        [HttpDelete("{chapterNum}", Name = "DeleteChapter")]
        public ActionResult<ChapterDto> DeleteSubchapterContent(int chapterNum)
        {
            try
            {

                var chapter = ContentDataStore.Instance.Contents.FirstOrDefault(chap => chap.Number == chapterNum);
                if (chapter == null)
                {
                    _logger.LogInformation($"Chapter number  {chapterNum}  wasn't found when accessing subchapters");
                    return NotFound();
                }

                ContentDataStore.Instance.Contents.Remove(chapter);

                _mailService.Send("Chapter Deleted", $"Chapter with id {chapter.Id} and title {chapter.Title} was deleted");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured while trying to delete chapter {chapterNum}", ex);
                return StatusCode(500, "An error occured when trying to process your request");
            }
        }

        // Delete a subchapter (admin) 
        [HttpDelete("{chapterNum}/{subChapterNum}", Name = "DeleteSubchapter")]
        public ActionResult<ChapterDto> DeleteSubchapterContent(int chapterNum, int subchapterNum)
        {
            try
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

                _mailService.Send("Chapter Created", $"Subchapter with id {subchapter.Id} and title {subchapter.Title} was deleted from chapter {chapterNum}");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured when trying to delete subchapter {subchapterNum} for chapter {chapterNum}", ex);
                return StatusCode(500, "An error occured while trying to process your request");
            }

        }

    }
}
