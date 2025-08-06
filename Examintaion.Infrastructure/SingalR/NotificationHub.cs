using Examintaion.Infrastructure.Helpers.UserHelpers;
using Microsoft.AspNetCore.SignalR;

namespace Examintaion.Infrastructure.SingalR
{
    public class NotificationHub : Hub
    {
        private readonly IUserHelper userHelper;

        public NotificationHub(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }


        public override Task OnConnectedAsync()
        {

            var userId = userHelper.GetUserId();
            if (userId != null)
            {
                UserConnectionManager.AddConnection(userId, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

    }
}
