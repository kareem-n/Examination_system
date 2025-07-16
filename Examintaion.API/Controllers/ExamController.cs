using System.Security.Claims;
using Examination.Application.DTOs.ExamDto;
using Examination.Application.Interfaces.ExamService;
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

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [Authorize(Roles = "student")]
        [HttpPost("take-exam/{subjectId}")]
        public async Task<IActionResult> StudentRequestExam([FromRoute] Guid subjectId)
        {

            var userid = User.FindFirstValue("name");

            var result = await _examService.GenerateStudentExam(subjectId, userid!);
            if (result == null)
            {
                return BadRequest(ApiResponse<object>.Error(StatusCodes.Status400BadRequest, "Failed to Take Exam, Try again later."));
            }

            return Ok(ApiResponse<ExamDto>.Success(StatusCodes.Status200OK, "Exam generated successfully.", result));
        }


    }
}
