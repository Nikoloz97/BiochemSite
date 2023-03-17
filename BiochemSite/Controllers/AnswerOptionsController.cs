using BiochemSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    // AnswerOptions = child resource of StatusQuestion
    [Route("api/StatusQuestion/{questionId}/Explanation")]
    [ApiController]
    public class AnswerOptionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<AnswerOptionsDto>> GetAnswerExplanationForStatusQuestion(int questionId)
        {
            var statusQuestion = StatusQuestionStore.Instance.StatusQuestions.FirstOrDefault(q => q.Id == questionId);

            if (statusQuestion == null)
            {
                return NotFound();
            }
            return Ok(statusQuestion.AnswerOptions);
        }


    }
}
