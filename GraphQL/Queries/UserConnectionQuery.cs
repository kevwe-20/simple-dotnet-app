namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Query")]
    public class UserConnectionQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.UserConnection> GetUserConnections([ScopedService] AppDBContext context)
        {
            return context.UserConnections;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single User Connection")]
        public Ampifan.Models.UserConnection? GetUserConnectionsById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the User")] Int64 id)
        {
            return context.UserConnections.Include(u => u.User).Include(u => u.Connections).ThenInclude(c => c.User).FirstOrDefault(c => c.Id == id);
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single User's connection ")]
        public Ampifan.Models.UserConnection? GetUserConnectionsByUserId([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the User")] Int64 userId)
        {
            return context.UserConnections.Include(u => u.User).Include(u => u.Connections).ThenInclude(c => c.User).FirstOrDefault(c => c.UserId == userId);
        }
    }
}
