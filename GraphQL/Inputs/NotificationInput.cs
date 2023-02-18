using Ampifan.Models;

namespace Ampifan.GraphQL.Inputs.Notification
{
    public record AddNotificationInput(string Message, Int64 UserId);
    public record AddNotificationPayload(Ampifan.Models.Notification Notification);
    public record UpdateNotificationInput(Int64 Id, MessageStatus Status, Int64 UserId);

}