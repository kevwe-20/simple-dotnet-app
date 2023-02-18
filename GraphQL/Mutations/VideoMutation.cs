namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.Video;
    using Ampifan.Models;

    [ExtendObjectType("Mutation")]
    public class VideoMutation
    {
        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddVideoPayload> AddVideo(AddVideoInput input, [ScopedService] AppDBContext context)
        {
            var video = new Video
            {
                Url = input.Url,
                UserId = input.UserId,
                Private = input.Private
            };
            context.Videos.Add(video);
            await context.SaveChangesAsync();

            // Postprocess the video
            return new AddVideoPayload(video);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddVideoPayload> UpdateVideo(UpdateVideoInput input, [ScopedService] AppDBContext context)
        {
            var updatedVideo = new Video
            {
                Id = input.Id,
                Url = input.Url,
                UserId = input.UserId,
                Private = input.Private
            };
            context.Videos.Update(updatedVideo);
            await context.SaveChangesAsync();
            return new AddVideoPayload(updatedVideo);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteVideo(Int64 id, [ScopedService] AppDBContext context)
        {
            var videoToDelete = await context.Videos.FindAsync(id);
            if (videoToDelete == null)
            {
                return "Invalid Operation";
            }

            context.Videos.Remove(videoToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }


    }
}
