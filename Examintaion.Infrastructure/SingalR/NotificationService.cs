using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Interfaces.Services;
using Examination.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Examintaion.Infrastructure.SingalR
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly INotoficationRepo notoficationRepo;

        public NotificationService(IHubContext<NotificationHub> hubContext, INotoficationRepo notoficationRepo)
        {
            this.hubContext = hubContext;
            this.notoficationRepo = notoficationRepo;
        }
        public async Task SendNotificationAsync(string userId, string message)
        {
            var connection = UserConnectionManager.GetConnections(userId);

            var not = new Notification
            {
                Message = message,
                UserId = userId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await notoficationRepo.AddNotificationAsync(not);
            foreach (var item in connection)
            {
                await hubContext.Clients.Client(item).SendAsync("ReceiveMessage", not);
            }

        }
    }
}
