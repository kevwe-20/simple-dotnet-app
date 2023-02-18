using Ampifan.Models.Expo;
using Ampifan.Services.Expo;

namespace Ampifan.Services
{
    public class NotificationService : INotificationService
    {
        Task INotificationService.SendEmailNotification()
        {
            throw new NotImplementedException();
        }

        async Task INotificationService.SendPushNotification(List<string> users, int badgeCount, string messageContent)
        {
            var expoSDKClient = new ExpoApiClient();
            var pushTicketReq = new PushTicketRequest()
            {
                PushTo = users,
                PushBadgeCount = badgeCount,
                PushBody = messageContent
            };
            var result = await expoSDKClient.PushSendAsync(pushTicketReq);

            if (result?.PushTicketErrors?.Count() > 0)
            {
                foreach (var error in result.PushTicketErrors)
                {
                    Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
                }
            }

        }


    }
}