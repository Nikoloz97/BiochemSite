using BiochemSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    // AnswerOptions = child resource of StatusQuestion
    [Route("api/StatusQuestion/{questionId}/AnswerOptions")]
    [ApiController]
    public class AnswerOptionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<AnswerOptionsDto>> GetAnswerOptionsForStatusQuestion(int questionId)
        {
            var answerOptions = StatusQuestionStore.Instance.StatusQuestions.FirstOrDefault(q => q.Id == questionId);

            if (answerOptions == null)
            {
                return NotFound();
            }

        }
    }
}
