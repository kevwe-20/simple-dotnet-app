namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;

    [ExtendObjectType("Query")]
    public class TalentQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.Talent> GetTalents([ScopedService] AppDBContext context)
        {
            return context.Talents;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single Talent")]
        public Ampifan.Models.Talent? GetTalentsById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the Talent")] Int64 id)
        {
            return context.Talents.FirstOrDefault(c => c.UserId == id);
        }
    }
}
