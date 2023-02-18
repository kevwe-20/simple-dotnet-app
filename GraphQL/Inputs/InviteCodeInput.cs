namespace Ampifan.GraphQL.Inputs.InviteCode
{
    public record AddInviteCodeInput(string Code);
    public record AddInviteCodePayload(Ampifan.Models.InviteCode InviteCode);
    public record UpdateInviteCodeInput(Int64 Id, string Code, bool Used);

}