namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Query")]
    public class NotificationQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.Notification> GetUserNotifications([ScopedService] AppDBContext context)
        {
            return context.Notifications;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single Notification")]
        public Ampifan.Models.Notification? GetUserConnectionsById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the User")] Int64 id)
        {
            return context.Notifications.Include(u => u.User).FirstOrDefault(c => c.Id == id);
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get all the User's Notification ")]
        public Ampifan.Models.Notification? GetUserConnectionsByUserId([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the User")] Int64 userId)
        {
            return context.Notifications.Include(u => u.User).OrderByDescending(n => n.DateSent).FirstOrDefault(c => c.UserId == userId);
        }
    }
}
