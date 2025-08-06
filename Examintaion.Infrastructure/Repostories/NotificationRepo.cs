using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Models;
using Examintaion.Infrastructure.Data;
using MongoDB.Driver;

namespace Examintaion.Infrastructure.Repostories
{
    public class NotificationRepo : INotoficationRepo
    {
        private readonly MongoDBContext mongoDBContext;

        public NotificationRepo(MongoDBContext mongoDBContext)
        {
            this.mongoDBContext = mongoDBContext;
        }
        public async Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken = default)
        {
            await mongoDBContext.GetCollection<Notification>("Notifications").InsertOneAsync(notification, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await mongoDBContext.GetCollection<Notification>("Notifications")
                .Find(x => x.IsRead == false)
                .ToListAsync();
        }

        public async Task MarkAsRead(string notifcationId)
        {
            await mongoDBContext.GetCollection<Notification>("Notifications")
                .UpdateOneAsync(
                    Builders<Notification>.Filter.Eq(x => x._Id, notifcationId),
                    Builders<Notification>.Update.Set(x => x.IsRead, true)
                );

        }
    }
}
