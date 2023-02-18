namespace Ampifan.GraphQL.Queries
{
    using HotChocolate;
    using HotChocolate.Types;

    [ExtendObjectType("Query")]
    public class CategoryQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.Category> GetCategories([ScopedService] AppDBContext context)
        {
            return context.Categories;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single Category")]
        public Ampifan.Models.Category? GetCategoriesById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the category")] Int64 id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
