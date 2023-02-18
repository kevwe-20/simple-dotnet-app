namespace Ampifan.GraphQL.Inputs.Video
{
    public record AddVideoInput(string Url, Int64 UserId, Boolean Private);
    public record AddVideoPayload(Ampifan.Models.Video Video);
    public record UpdateVideoInput(Int64 Id, string Url, Int64 UserId, Boolean Private);

}