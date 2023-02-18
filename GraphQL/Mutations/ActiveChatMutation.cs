namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.ActiveChatInput;
    using Ampifan.Models;
    using Microsoft.EntityFrameworkCore;
    using Ampifan.Services;

    [ExtendObjectType("Mutation")]
    public class ActiveChatMutation
    {
        private readonly INotificationService _notificationService;

        public ActiveChatMutation(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [UseDbContext(typeof(AppDBContext))]
        public async Task<SendMessagePayload> SendMessage(SendMessageInput input, [ScopedService] AppDBContext context)
        {
            // Check if user creating connection exists
            var userExist = context.Users.Where(c => c.Id == input.UserId).FirstOrDefault();
            if (userExist == null)
            {
                throw new GraphQLException("Cannot find user");
            }

            // Check if user to connect with exists
            var userConnectionExist = context.Users.Where(c => c.Id == input.ChatUserId).FirstOrDefault();
            if (userConnectionExist == null)
            {
                throw new GraphQLException("Cannot find user to connect with");
            }


            var chatExists = context.ActiveChats.Include(c => c.ChatMessages).ThenInclude(x => x.User).Where(c => c.UserId == input.UserId).FirstOrDefault();
            if (chatExists != null)
            {
                // check if user is already connected
                if (chatExists.ChatMessages.Count > 0)
                {
                    // Add user message
                    chatExists.ChatMessages.Add(new ChatMessage
                    {
                        UserId = input.ChatUserId,
                        Message = input.Message,
                        DateSent = DateTime.Now,
                        Status = MessageStatus.Delivered,
                        ActiveChatId = chatExists.Id
                    });
                    context.ActiveChats.Update(chatExists);
                }

            }
            else
            {
                // Create userConnection
                chatExists = new ActiveChat
                {
                    UserId = input.UserId
                };

                chatExists.ChatMessages = new List<ChatMessage>();

                // Add user message
                chatExists.ChatMessages.Add(new ChatMessage
                {
                    UserId = input.ChatUserId,
                    Message = input.Message,
                    DateSent = DateTime.Now,
                    Status = MessageStatus.Delivered,
                    ActiveChatId = chatExists.Id
                });
                context.ActiveChats.Add(chatExists);
            }

            await context.SaveChangesAsync();
            chatExists.ChatMessages = chatExists.ChatMessages.Where(x => x.UserId == input.ChatUserId).OrderByDescending(c => c.DateSent).ToList();

            await _notificationService.SendPushNotification(
                               new List<string> { userConnectionExist.PushToken },
                               1,
                               String.Format("New Message - {0} - {1}", userConnectionExist.Name, input.Message.Substring(0, 20))
                           );

            return new SendMessagePayload(chatExists);
        }


        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteChat(Int64 id, [ScopedService] AppDBContext context)
        {
            var userToDelete = await context.ActiveChats.FindAsync(id);
            if (userToDelete == null)
            {
                return "Invalid Operation";
            }

            context.ActiveChats.Remove(userToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }

    }
}
