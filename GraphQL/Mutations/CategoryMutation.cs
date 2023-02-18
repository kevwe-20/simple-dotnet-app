namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.Category;
    using Ampifan.Models;

    [ExtendObjectType("Mutation")]
    public class CategoryMutation
    {
        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddCategoryPayload> AddCategory(AddCategoryInput input, [ScopedService] AppDBContext context)
        {
            var category = new Category
            {
                Name = input.Name
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return new AddCategoryPayload(category);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddCategoryPayload> UpdateCategory(UpdateCategoryInput input, [ScopedService] AppDBContext context)
        {
            var updatedCategory = new Category
            {
                Id = input.Id,
                Name = input.Name,
                Active = input.Active
            };
            context.Categories.Update(updatedCategory);
            await context.SaveChangesAsync();
            return new AddCategoryPayload(updatedCategory);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteCategory(Int64 id, [ScopedService] AppDBContext context)
        {
            var categoryToDelete = await context.Categories.FindAsync(id);
            if (categoryToDelete == null)
            {
                return "Invalid Operation";
            }

            context.Categories.Remove(categoryToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }


    }
}
