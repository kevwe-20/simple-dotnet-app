namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Query")]
    public class ActiveChatQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.ActiveChat> GetActiveChats([ScopedService] AppDBContext context)
        {
            return context.ActiveChats;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single ActiveChat")]
        public Ampifan.Models.ActiveChat? GetActiveChatsById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the ActiveChat")] Int64 id)
        {
            return context.ActiveChats.Include(c => c.User).Include(c => c.ChatMessages).ThenInclude(c => c.User).FirstOrDefault(c => c.Id == id);
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a users Active Chats")]
        public IQueryable<Ampifan.Models.User>? GetActiveChatUsersByUserId([ScopedService] AppDBContext context, [GraphQLDescription("The UserId to retrieve chat for")] Int64 userId)
        {
            // Get the chat Messages for the user
            var chatExists = context.ActiveChats.Include(c => c.User).Include(c => c.ChatMessages).ThenInclude(c => c.User).FirstOrDefault(c => c.UserId == userId);

            if (chatExists != null)
            {
                // filter unique chats and order by DateTime Received
                var activeChatUsers = chatExists.ChatMessages.OrderByDescending(d => d.DateSent).Select(d => d.UserId).Distinct().ToList();

                // return the users
                var activeChatUserList = context.Users.Where(u => activeChatUsers.Contains(u.Id));
                return activeChatUserList;
            }
            return null;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a users Active Chats")]
        public Ampifan.Models.ActiveChat? GetActiveChatsByUserId([ScopedService] AppDBContext context, [GraphQLDescription("The UserId to retrieve chat for")] Int64 userId)
        {
            // Get the chat Messages for the user
            var chatExists = context.ActiveChats.Include(c => c.User).Include(c => c.ChatMessages).ThenInclude(c => c.User).FirstOrDefault(c => c.UserId == userId);

            if (chatExists != null)
            {
                // filter unique chats and order by DateTime Received
                chatExists.ChatMessages = chatExists.ChatMessages.OrderByDescending(c => c.DateSent).ToList();

                // return the chats
                return chatExists;
            }
            return null;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a users Active Chats")]
        public Ampifan.Models.ActiveChat? GetActiveChatsBetweenUsers([ScopedService] AppDBContext context, [GraphQLDescription("The UserId to retrieve chat for")] Int64 userId,[GraphQLDescription("The UserId to retrieve chat with")] Int64 chatUserId )
        {
            // Get the chat Messages for the user
            var chatExists = context.ActiveChats.Include(c => c.User).Include(c => c.ChatMessages).ThenInclude(c => c.User).FirstOrDefault(c => c.UserId == userId);

            if (chatExists != null)
            {
                // filter unique chats and order by DateTime Received
                chatExists.ChatMessages = chatExists.ChatMessages.Where(x => x.UserId == chatUserId).OrderByDescending(c => c.DateSent).ToList();

                // return the chats
                return chatExists;
            }
            return null;
        }
    }
}
