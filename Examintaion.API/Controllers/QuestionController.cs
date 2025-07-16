using Examination.Application.DTOs.Question;
using Examination.Application.Interfaces.QuestionService;
using Microsoft.AspNetCore.Mvc;
using Template.API.Response;

namespace Examination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllQuestions([FromQuery] GetAllQuestionsParams @params)
        {

            var result = await questionService.GetAllQuestions(@params);

            if (result is null || !result.Any())
            {
                return NotFound(ApiResponse<object>.Error(404, "No Questions Found"));
            }
            return Ok(ApiResponse<IEnumerable<QuestionDto>>.Success(200, "success", result));

        }


        [HttpGet("SubjectQuestions/{subjectId}")]
        public async Task<IActionResult> GetSubjectQuestions([FromRoute] Guid subjectId)
        {
            var result = await questionService.GetSubjectQuestions(subjectId);
            if (result is null || !result.Any())
            {
                return NotFound(ApiResponse<object>.Error(404, "No Questions Found for this Subject"));
            }
            return Ok(ApiResponse<IEnumerable<QuestionDto>>.Success(200, "success", result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto questionDto)
        {
            if (questionDto == null)
            {
                return BadRequest(ApiResponse<object>.Error(400, "Invalid question data"));
            }

            if (questionDto.Choices.Count != 4)
            {
                return BadRequest(ApiResponse<object>.Error(400, "Question must have exactly 4 choices"));
            }

            if (questionDto.Choices.Count(c => c.IsCorrect) > 1)
            {
                return BadRequest(ApiResponse<object>.Error(400, "Only one choice can be marked as correct"));
            }

            var createdQuestion = await questionService.CreateQuestion(questionDto);

            if (createdQuestion == null)
            {
                return StatusCode(500, ApiResponse<object>.Error(500, "An error occurred while creating the question"));
            }


            return CreatedAtAction(nameof(GetAllQuestions), new { id = createdQuestion.Id }, ApiResponse<QuestionDto>.Success(201, "Question created successfully", createdQuestion));
        }

    }
}
