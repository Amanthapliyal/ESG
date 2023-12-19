using ESG_App.Common;
using ESG_App.DTO.Response;
using ESG_App.IService;
using ESG_App.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ESG_App.Controllers
{
    [ApiController]
    [Route("Coedify/api/esg/survey-questions")]
    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyQuestionService _surveyQuestionService;

        public SurveyQuestionController(ISurveyQuestionService surveyQuestionService)
        {
            _surveyQuestionService = surveyQuestionService;


        }

        [HttpPost("surveysId")]
        public async Task<IActionResult> addingSurveyQuestion([FromQuery] int surveyId, [FromBody] List<int> questionList)
        {
            var result = await _surveyQuestionService.addSurveyQuestion(surveyId, questionList);

            return Ok(BaseResponse<SurveyQuestionsPostResponse>.Success(result));
        }

        [HttpGet]
        public async Task<IActionResult> gettingAllSurveyQuestion()
        {
            var result = await _surveyQuestionService.getAllSurveyQuestions();

            return Ok(BaseResponse<List<SurveyQuestionsResponse>>.Success(result));

        }

        [HttpPut("surveyId")]
        public async Task<IActionResult> updatingSurveyQuestion([FromQuery] int surveyId, [FromBody] List<int> questionList)
        {
            var result = await _surveyQuestionService.updateSurveyQuestion(surveyId, questionList);

            return Ok(BaseResponse<SurveyQuestionsPostResponse>.Success(result));

        }

        [HttpDelete("surveyId")]

        public async Task<IActionResult> removeSurveyQuestion([FromQuery] int surveyId)
        {
            var result = await _surveyQuestionService.removeSurveyQuestionBySurveyId(surveyId);

            return Ok(BaseResponse<SurveyQuestionsPostResponse>.Success(result));

        }

    }
}
