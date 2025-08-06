using Examination.API.Extenstions;
using Examination.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public TestController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }


        [HttpGet("test")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            var userId = User.GetUserId();
            var message = new
            {
                Title = "Test Notification",
                Body = "This is a test notification.",
                UserId = userId
            };
            await notificationService.SendNotificationAsync(userId, "Test notify");
            return Ok(0);
        }
    }
}
