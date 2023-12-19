using ESG_App.Common;
using ESG_App.DTO.Request;
using ESG_App.Model;
using ESG_App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ESG_App.Controllers
{

    [ApiController]
    [Route("Coedify/api/esg/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
         
        }

        [HttpPost]
        public async Task<IActionResult> addingQuestion(Question request)
        {
            var result = await _questionService.addQuestions(request);

            return Ok(BaseResponse<Question>.Success(result));
        }

        [HttpGet]
        public async Task<IActionResult> GettingAllQuestion()
        {
            var result = await _questionService.getAllQuestions();

            return Ok(BaseResponse<List<Question>>.Success(result));
        }

        [HttpGet("id")]
        public async Task<IActionResult> gettingQuestionById([FromQuery] int QuestionId)
        {
            var result = await _questionService.getQuestionsById(QuestionId);

            return Ok(BaseResponse<Question>.Success(result));
        }

        [HttpDelete("id")]

        public async Task<IActionResult> deletingQuestionById([FromQuery] int QuestionId)
        {
            var result = await _questionService.removeQuestionById(QuestionId);

            return Ok(BaseResponse<Question>.Success(result));
        }

        [HttpPut("id")]

        public async Task<IActionResult> updatingQuestion([FromQuery] int QuestionId, [FromBody] QuestionUpdateDTO questionUpdateDTO)
        {
            var result = await _questionService.updateQuestion(QuestionId, questionUpdateDTO);

            return Ok(BaseResponse<Question>.Success(result));
        }
    }
}
