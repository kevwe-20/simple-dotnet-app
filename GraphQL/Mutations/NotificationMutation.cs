namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.Notification;
    using Ampifan.Models;
    using Ampifan.Services;

    [ExtendObjectType("Mutation")]
    public class NotificationMutation
    {
        private readonly INotificationService _notificationService;

        public NotificationMutation(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddNotificationPayload> AddNotification(AddNotificationInput input, [ScopedService] AppDBContext context)
        {
            var notification = new Notification
            {
                DateSent = DateTime.Now,
                UserId = input.UserId,
                Message = input.Message
            };
            context.Notifications.Add(notification);
            await context.SaveChangesAsync();

            // Get User 
            var user = context.Users.Find(input.UserId);

            // Send App Notification
            if (user != null && !String.IsNullOrEmpty(user.PushToken))
            {
                await _notificationService.SendPushNotification(
                    new List<string> { user.PushToken },
                    1,
                    "You have a new notification"
                );
            }

            return new AddNotificationPayload(notification);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddNotificationPayload> MarkAsRead(UpdateNotificationInput input, [ScopedService] AppDBContext context)
        {
            var updatedNotification = new Notification
            {
                Id = input.Id,
                Status = input.Status
            };
            context.Notifications.Update(updatedNotification);
            await context.SaveChangesAsync();
            return new AddNotificationPayload(updatedNotification);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteNotification(Int64 id, [ScopedService] AppDBContext context)
        {
            var notificationToDelete = await context.Notifications.FindAsync(id);
            if (notificationToDelete == null)
            {
                return "Invalid Operation";
            }

            context.Notifications.Remove(notificationToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }


    }
}
