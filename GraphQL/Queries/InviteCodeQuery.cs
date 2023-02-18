namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;

    [ExtendObjectType("Query")]
    public class InviteCodeQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.InviteCode> GetInviteCodes([ScopedService] AppDBContext context)
        {
            return context.InviteCodes;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single InviteCode")]
        public Ampifan.Models.InviteCode? GetInviteCodesById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the InviteCode")] Int64 id)
        {
            return context.InviteCodes.FirstOrDefault(c => c.Id == id);
        }
    }
}
