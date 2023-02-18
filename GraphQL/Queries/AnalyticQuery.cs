namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;

    [ExtendObjectType("Query")]
    public class AnalyticQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.Analytic> GetAnalytics([ScopedService] AppDBContext context)
        {
            return context.Analytics;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single Analytic")]
        public Ampifan.Models.Analytic? GetAnalyticsById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the Analytic")] Int64 id)
        {
            return context.Analytics.FirstOrDefault(c => c.Id == id);
        }
    }
}
