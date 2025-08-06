using Examination.API.Extenstions;
using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Template.API.Response;

namespace Examination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotoficationRepo notoficationRepo;

        public NotificationController(INotoficationRepo notoficationRepo)
        {
            this.notoficationRepo = notoficationRepo;
        }

        //[Authorize]
        [HttpGet("notifcations")]
        public async Task<IActionResult> GetUserNotifcation()
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            var r = await notoficationRepo.GetNotificationsAsync(userId);
            if (!r.Any())
            {
                return NotFound(ApiResponse<object>.Error(StatusCodes.Status404NotFound, "No Notifications"));
            }

            return Ok(ApiResponse<IEnumerable<Notification>>.Success(StatusCodes.Status200OK, "Notifications", r));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add()
        {
            var s = new Notification
            {
                Message = "Test Notification",
                UserId = "12345", // Replace with actual user ID
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await notoficationRepo.AddNotificationAsync(s);
            return Ok();
        }

        [HttpPost("MarkAsRead/{notifcationId}")]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            await notoficationRepo.MarkAsRead(id);
            return Ok(ApiResponse<object>.Success(StatusCodes.Status200OK, "success"));
        }

    }
}
