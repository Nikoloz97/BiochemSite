using BiochemSite.Models.Explanation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    // Explanation = child resource of StatusQuestion
    [Route("api/StatusExam/{questionId}/Explanation")]
    [ApiController]
    public class ExplanationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ExplanationDto>> GetExplanationForStatusQuestion(int questionId)
        {
            var statusQuestion = StatusQuestionStore.Instance.StatusQuestions.FirstOrDefault(q => q.Id == questionId);

            if (statusQuestion == null)
            {
                return NotFound();
            }
            return Ok(statusQuestion.Explanation);
        }


    }
}
