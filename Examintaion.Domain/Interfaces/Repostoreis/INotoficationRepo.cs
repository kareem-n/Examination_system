using Examination.Domain.Models;

namespace Examination.Domain.Interfaces.Repostoreis
{
    public interface INotoficationRepo
    {
        Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> GetNotificationsAsync(string userId, CancellationToken cancellationToken = default);

        Task MarkAsRead(string notifcationId);

    }
}
