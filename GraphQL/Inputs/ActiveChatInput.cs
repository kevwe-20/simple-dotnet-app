namespace Ampifan.GraphQL.Inputs.ActiveChatInput
{
    public record SendMessageInput(Int64 UserId,Int64 ChatUserId, String Message);
    public record SendMessagePayload(Ampifan.Models.ActiveChat ActiveChat);
    public record UpdateUserConnectionInput(Int64 UserId,Int64 ChatUserId);

}