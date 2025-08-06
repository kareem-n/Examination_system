using Examination.API.Extenstions;
using Examination.Application.DTOs.Exam;
using Examination.Application.DTOs.ExamDto;
using Examination.Application.Interfaces.ExamService;
using Examination.Domain.Common;
using Examination.Domain.Interfaces.Repostoreis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.API.Response;

namespace Examination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly IMessagePublisher messagePublisher;

        public ExamController(IExamService examService, IMessagePublisher messagePublisher)
        {
            _examService = examService;
            this.messagePublisher = messagePublisher;
        }



        [Authorize(Roles = "student")]
        [HttpPost("take-exam/{subjectId}")]
        public async Task<IActionResult> StudentRequestExam([FromRoute] Guid subjectId)
        {
            var userId = User.GetUserId();

            var result = await _examService.GenerateStudentExam(subjectId, userId);
            if (result == null)
            {
                return BadRequest(ApiResponse<object>.Error(StatusCodes.Status400BadRequest, "Failed to Take Exam, Try again later."));
            }

            return Ok(ApiResponse<ExamDto>.Success(StatusCodes.Status200OK, "Exam generated successfully.", result));
        }


        [HttpPost("submit-exam/{ExamId}")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> SubmitExam([FromRoute] Guid ExamId, [FromBody] ExamAnswers examAnswers)
        {
            var x = await _examService.SubmitExam(ExamId, examAnswers);

            if (x)
            {
                return Ok(ApiResponse<object>.Success(200, "Exam Submmited Success"));
            }

            return BadRequest(ApiResponse<object>.Success(400, "something went wrong"));
        }

        [HttpGet("GetUserExams")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetUsersExams([FromQuery] UserExamsHistoryParams userExamsHistoryParams)
        {
            var userId = User.GetUserId();
            var exams = await _examService.GetUserExams(userId, userExamsHistoryParams);
            if (exams == null || !exams.Items.Any())
            {
                return NotFound(ApiResponse<object>.Error(StatusCodes.Status404NotFound, "No exams found for the user."));
            }
            return Ok(ApiResponse<PageModel<UserExamDto>>.Success(StatusCodes.Status200OK, "Exams retrieved successfully.", exams));


        }
    }
}
