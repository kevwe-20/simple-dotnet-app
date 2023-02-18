namespace Ampifan.GraphQL.Inputs.UserConnectionInput
{
    public record AddUserConnectionInput(Int64 UserId,Int64 ConnectionUserId, Boolean isFollowing );
    public record AddUserConnectionPayload(Ampifan.Models.UserConnection UserConnection);
    public record UpdateUserConnectionInput(Int64 UserId,Int64 ConnectionUserId);

}