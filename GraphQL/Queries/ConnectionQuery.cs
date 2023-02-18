namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Query")]
    public class ConnectionQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.Connection> GetConnections([ScopedService] AppDBContext context)
        {
            return context.Connections;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single Connection")]
        public Ampifan.Models.Connection? GetConnectionsById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the Connection")] Int64 id)
        {
            return context.Connections.Include(c => c.User).FirstOrDefault(c => c.Id == id);
        }
    }
}
