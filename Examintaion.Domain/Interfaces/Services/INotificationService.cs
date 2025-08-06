namespace Examination.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string message, string recipientEmail);
    }
}
