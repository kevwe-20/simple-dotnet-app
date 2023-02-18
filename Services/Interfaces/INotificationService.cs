namespace Ampifan.Services
{
    interface INotificationService {
        Task SendPushNotification(List<string> users, int badgeCount, string messageContent );
        Task SendEmailNotification();
    }
}