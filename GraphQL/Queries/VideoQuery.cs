namespace Ampifan.GraphQL.Queries
{
    using System.Linq;
    using HotChocolate;
    using HotChocolate.Types;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Query")]
    public class VideoQuery
    {

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection] 
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.Video> GetVideos([ScopedService] AppDBContext context)
        {
            return context.Videos;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UsePaging]
        [UseProjection] 
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ampifan.Models.Video> GetUserVideos([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the User")] Int64 userId)
        {
            return context.Videos.Where(v => v.UserId == userId);
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection]
        [GraphQLDescription("Get a single Videos")]
        public Ampifan.Models.Video? GetVideosById([ScopedService] AppDBContext context, [GraphQLDescription("The Id of the Video")] Int64 id)
        {
            return context.Videos.Include(v => v.User).FirstOrDefault(c => c.Id == id);
        }
    }
}
