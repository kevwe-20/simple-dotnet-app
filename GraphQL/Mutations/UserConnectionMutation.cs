namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.UserConnectionInput;
    using Ampifan.Models;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Mutation")]
    public class UserConnectionMutation
    {
        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddUserConnectionPayload> AddConnection(AddUserConnectionInput input, [ScopedService] AppDBContext context)
        {
            // Check if user creating connection exists
            var userExist = context.Users.Where(c => c.Id == input.UserId).FirstOrDefault();
            if (userExist == null)
            {
                throw new GraphQLException("Cannot find user");
            }

            // Check if user to connect with exists
            var userConnectionExist = context.Users.Where(c => c.Id == input.ConnectionUserId).FirstOrDefault();
            if (userConnectionExist == null)
            {
                throw new GraphQLException("Cannot find user to connect with");
            }


            var connectionExist = context.UserConnections.Include(c => c.Connections).ThenInclude(x => x.User).Where(c => c.UserId == input.UserId).FirstOrDefault();
            if (connectionExist != null)
            {
                // check if user is already connected
                if (connectionExist.Connections.Count > 0)
                {
                    var isAlreadyConnected = connectionExist.Connections.Where(x => x.UserId == input.ConnectionUserId).FirstOrDefault();
                    if (isAlreadyConnected == null)
                    {
                        // Add user connection
                        connectionExist.Connections.Add(new Connection
                        {
                            UserId = input.ConnectionUserId,
                            DateConnected =  DateTime.Now,
                            isFollowing = input.isFollowing,
                            UserConnectionId = connectionExist.Id
                        });
                        context.UserConnections.Update(connectionExist);
                    }
                }

            }
            else
            {
                // Create userConnection
                connectionExist = new UserConnection
                {
                    UserId = input.UserId
                };

                connectionExist.Connections = new List<Connection>();

                // Add user connection
                connectionExist.Connections.Add(new Connection
                {
                    UserId = input.ConnectionUserId,
                    DateConnected =  DateTime.Now,
                    isFollowing = input.isFollowing,
                });
                context.UserConnections.Add(connectionExist);
            }

            await context.SaveChangesAsync();
            return new AddUserConnectionPayload(connectionExist);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddUserConnectionPayload> UpdateConnection(UpdateUserConnectionInput input, [ScopedService] AppDBContext context)
        {
            // Check if user with email exists
            var connectionExist = context.UserConnections.Include(c => c.User).Include(c => c.Connections).ThenInclude(x => x.User).Where(c => c.UserId == input.UserId).FirstOrDefault();
            if (connectionExist != null)
            {
                // check if user is already connected
                if (connectionExist.Connections.Count > 0)
                {
                    var isAlreadyConnected = connectionExist.Connections.Where(x => x.UserId == input.ConnectionUserId).FirstOrDefault();
                    if (isAlreadyConnected != null)
                    {
                        // Remove user connection
                        connectionExist.Connections.Remove(isAlreadyConnected);
                    }
                    context.UserConnections.Update(connectionExist);
                }
                await context.SaveChangesAsync();
                return new AddUserConnectionPayload(connectionExist);
            }
            return null;
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteConnection(Int64 id, [ScopedService] AppDBContext context)
        {
            var userToDelete = await context.UserConnections.FindAsync(id);
            if (userToDelete == null)
            {
                return "Invalid Operation";
            }

            context.UserConnections.Remove(userToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }

    }
}
