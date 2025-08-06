using System.Security.Claims;

namespace Examination.API.Extenstions
{
    public static class UserContext
    {

        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            var userId = user.FindFirstValue("name");
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User ID not found in claims");
            }
            return userId;
        }


    }
}
