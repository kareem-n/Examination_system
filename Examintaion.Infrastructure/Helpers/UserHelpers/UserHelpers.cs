using Microsoft.AspNetCore.Http;

namespace Examintaion.Infrastructure.Helpers.UserHelpers
{
    public class UserHelpers : IUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelpers(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
         => _httpContextAccessor?.HttpContext?.User?.FindFirst("name")?.Value ?? string.Empty;

    }
}
