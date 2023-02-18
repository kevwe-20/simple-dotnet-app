namespace Ampifan.GraphQL.Inputs.Category
{
    public record AddCategoryInput(string Name);
    public record AddCategoryPayload(Ampifan.Models.Category Category);
    public record UpdateCategoryInput(Int64 Id, string Name, bool Active);

}