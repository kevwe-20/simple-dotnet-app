namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Query")]
    public class UserQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.User> GetUsers([ScopedService] AppDBContext context)
        {
            return context.Users;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single User")]
        public Ampifan.Models.User? GetUsersById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the User")] Int64 id)
        {
            return context.Users.Include(u => u.Videos).FirstOrDefault(c => c.Id == id);
        }
    }
}
