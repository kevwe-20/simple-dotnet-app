namespace Ampifan.GraphQL.Inputs.Talent
{
    public record AddTalentInput(string Name, string Email, string Bio, string PhotoUrl, string PushToken, Int64 Price, Int64 DeliveryTime, Boolean HasSale, Int64 CategoryId);
    public record AddTalentPayload(Ampifan.Models.Talent talent);
    public record UpdateTalentInput(Int64 Id, string Name, string Email, string Bio, string PhotoUrl, string PushToken, Int64 Price, Int64 DeliveryTime, Boolean HasSale, Int64 CategoryId);
}