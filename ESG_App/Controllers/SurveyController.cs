using ESG_App.Common;
using ESG_App.DTO.Request;
using ESG_App.ImplService;
using ESG_App.IService;
using ESG_App.Model;
using ESG_App.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ESG_App.Controllers
{
    [ApiController]
    [Route("Coedify/api/esg/surveys")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;

        }

        [HttpPost]
        public async Task<IActionResult> addingSurvey(Survey request)
        {
            var result = await _surveyService.addSurvey(request);

            return Ok(BaseResponse<Survey>.Success(result));
        }

        [HttpGet]
        public async Task<IActionResult> GettingAllSurvey()
        {
            var result = await _surveyService.getAllSurvey();

            return Ok(BaseResponse<List<Survey>>.Success(result));
        }

        [HttpGet("id")]
        public async Task<IActionResult> gettingSurveyById([FromQuery] int surveyId)
        {
            var result = await _surveyService.getSurveyById(surveyId);

            return Ok(BaseResponse<Survey>.Success(result));
        }

        [HttpDelete("id")]

        public async Task<IActionResult> deletingSurveyById([FromQuery] int surveyId)
        {
            var result = await _surveyService.removeSurveyById(surveyId);

            return Ok(BaseResponse<Survey>.Success(result));
        }

        [HttpPut("id")]

        public async Task<IActionResult> updatingSurvey([FromQuery] int surveyId, [FromBody] SuveyUpdateDTO surveyUpdateDTO)
        {
            var result = await _surveyService.updateSurveyById(surveyId, surveyUpdateDTO);

            return Ok(BaseResponse<Survey>.Success(result));
        }
    }
}
