﻿using BiochemSite.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
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
            var chapterContent = ContentDataStore.Instance.Contents.Where(c => c.Number == chapterNum);

            return Ok(chapterContent);
        }

        // Content for a subchapter
        [HttpGet("{chapterNum}/{subChapterNum}", Name = "GetSubchapter")]
        public ActionResult<ChapterDto> GetSubchapterContent(int chapterNum, int subchapterNum)
        {
            var correctChapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => (c.Number == chapterNum));

            if (correctChapter == null)
            {
                return NotFound();
            }

            var subchapterContent = correctChapter.Subchapters.FirstOrDefault(sc => (sc.Number == subchapterNum));

            if (subchapterContent == null)
            {
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
        [HttpPost("{chapterNum}")]
        public ActionResult<SubchapterDto> CreateSubchapterContent(int chapterNum, SubchapterForCreationDto chapterToAdd)
        {
            var CorrespondingChapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => (c.Number == chapterNum));

            if (CorrespondingChapter == null)
            {
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
        [HttpPut("{chapterNum}")]
        public ActionResult UpdateChapterContent(int chapterNum, ChapterForUpdateDto updatedChapter)
        {
            var chapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => c.Number == chapterNum);

            if (chapter == null)
            {
                return NotFound();
            }

            chapter.Number = updatedChapter.Number;
            chapter.Title = updatedChapter.Title;
            chapter.Subchapters = updatedChapter.Subchapters;

            return NoContent();
        }

        // Edit a subchapter (admin)
        [HttpPut("{chapterNum}/{subchapterNum}")]
        public ActionResult UpdateSubchapterContent(int chapterNum, int subchapterNum, SubchapterForUpdateDto updatedChapter)
        {
            var correspondingChapter = ContentDataStore.Instance.Contents.FirstOrDefault(c => (c.Number == chapterNum));

            if (correspondingChapter == null)
            {
                return NotFound();
            }

            var subchapter = correspondingChapter.Subchapters.FirstOrDefault(sc => (sc.Number == subchapterNum));

            if (subchapter == null)
            {
                return NotFound();
            }

            subchapter.Number = updatedChapter.Number;
            subchapter.Title = updatedChapter.Title;
            subchapter.Paragraphs = updatedChapter.Paragraphs;

            return NoContent();
        }






        // Edit a chapter (admin)
        [HttpPatch("{chapterNum}")]
        public ActionResult<ChapterDto> EditChapterContent(int chapterNum)
        {
            return Ok();
        }

        // Edit a subchapter (admin)
        [HttpPatch("{chapterNum}/{subChapterNum}")]
        public ActionResult<ChapterDto> EditSubchapterContent(int chapterNum, int subchapterNum)
        {
            return Ok();
        }

        // Delete a chapter (admin) 
        [HttpDelete("{chapterNum}")]
        public ActionResult<ChapterDto> DeleteSubchapterContent(int chapterNum)
        {
            return Ok();
        }

        // Delete a subchapter (admin) 
        [HttpDelete("{chapterNum}/{subChapterNum}")]
        public ActionResult<ChapterDto> DeleteSubchapterContent(int chapterNum, int subchapterNum)
        {
            return Ok();
        }

    }
}
