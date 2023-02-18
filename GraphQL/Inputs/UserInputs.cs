namespace Ampifan.GraphQL.Inputs.User
{
    public record AddUserInput(string Name, string Email, string Bio, string PhotoUrl, string PushToken);
    public record AddUserPayload(Ampifan.Models.User User);
    public record UpdateUserInput(Int64 Id, string Name, string Email, string Bio, string PhotoUrl, string PushToken);

}