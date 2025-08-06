using Examination.Application.DTOs.Dashboard;
using Examination.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Template.API.Response;

namespace Examination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        //[Authorize("admin")]
        [HttpGet("GetUsersPerMonth")]
        public async Task<IActionResult> GetUsersNumbers()
        {

            var result = await dashboardService.GetUserNumbers();


            if (result == null)
            {
                return BadRequest(new { Status = "Error", Message = "Failed to retrieve user numbers." });
            }

            return Ok(ApiResponse<UsersNumbersDto>.Success(200, "users", result));

        }

        //[Authorize("admin")]
        [HttpGet("GetTopExamsSubjetc")]
        public async Task<IActionResult> GetTopExamsSubjetc()
        {

            var result = await dashboardService.GetTopSubjectsExams();


            if (result == null)
            {
                return BadRequest(new { Status = "Error", Message = "Failed to retrieve user numbers." });
            }

            return Ok(ApiResponse<object>.Success(200, "users", result));

        }

        //[Authorize("admin")]
        [HttpGet("GetScoreCount")]
        public async Task<IActionResult> GetScoreCount()
        {

            var result = await dashboardService.GetExamsRate();


            if (result == null)
            {
                return BadRequest(new { Status = "Error", Message = "Failed to retrieve user numbers." });
            }

            return Ok(ApiResponse<object>.Success(200, "users", result));

        }

        //[Authorize("admin")]
        [HttpGet("GetTotals")]
        public async Task<IActionResult> GetTotals()
        {

            var result = await dashboardService.GetTotalStatus();


            if (result == null)
            {
                return BadRequest(new { Status = "Error", Message = "Failed to retrieve user numbers." });
            }

            return Ok(ApiResponse<object>.Success(200, "users", result));

        }
    }
}
