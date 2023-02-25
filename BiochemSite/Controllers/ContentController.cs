using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    public class ContentController : ControllerBase
    {
        public JsonResult GetChapterContent()
        {
            new JsonResult(
                new List<object>
                {
                    new {id = 1, ChapterNum = 1, SubChapNum = 1 ChapDesc = "Introduction to essential Amino acids"},
                    new {id = 2,ChapterNum = 1, SubChapNum = 2, ChapDesc = "Learning "},
                    new {id = 3, ChapterNum = 2,SubChapNum = 1, ChapDesc = },
                    new {id = 4, ChapterNum = 2,SubChapNum = 2, ChapDesc = }
                })
        }

    }
}
