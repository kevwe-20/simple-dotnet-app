namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.Talent;
    using Ampifan.Models;
    using Microsoft.EntityFrameworkCore;

    [ExtendObjectType("Mutation")]
    public class TalentMutation
    {
        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddTalentPayload> AddTalent(AddTalentInput input, [ScopedService] AppDBContext context)
        {
            // Check if user with email exists
            var userExist = context.Users.Where(c => c.Email == input.Email).FirstOrDefault();
            if (userExist != null)
            {
                throw new GraphQLException("Email already exists");
            }

            using var transaction = context.Database.BeginTransaction();
            try
            {
                var user = new User
                {
                    Name = input.Name,
                    Email = input.Email,
                    Bio = input.Bio,
                    PhotoUrl = input.PhotoUrl,
                    PushToken = input.PushToken,
                };

                var talent = new Talent
                {
                    Price = input.Price,
                    HasSale = input.HasSale,
                    IsFeatured = false,
                    DeliveryTime = input.DeliveryTime,
                    CategoryId = input.CategoryId,
                    User = user

                };
                context.Talents.Add(talent);
                await context.SaveChangesAsync();

                transaction.Commit();
                return new AddTalentPayload(talent);
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddTalentPayload> UpdateTalent(UpdateTalentInput input, [ScopedService] AppDBContext context)
        {
            var talentToUpdate = await context.Talents.Include(t => t.User).Where(t => t.Id == input.Id).FirstOrDefaultAsync();
            if (talentToUpdate != null)
            {
                talentToUpdate.Price = input.Price;
                talentToUpdate.HasSale = input.HasSale;
                talentToUpdate.DeliveryTime = input.DeliveryTime;
                talentToUpdate.CategoryId = input.CategoryId;
                talentToUpdate.User.Name = input.Name;
                talentToUpdate.User.Bio = input.Bio;
                talentToUpdate.User.PhotoUrl = input.PhotoUrl;
                talentToUpdate.User.PushToken = input.PushToken;

                context.Talents.Update(talentToUpdate);
                await context.SaveChangesAsync();
                return new AddTalentPayload(talentToUpdate);
            }

            throw new GraphQLException("Unable to update record!");
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteTalent(Int64 userId, [ScopedService] AppDBContext context)
        {
            var talentToDelete = await context.Talents.Where(t => t.UserId == userId).FirstOrDefaultAsync();
            if (talentToDelete == null)
            {
                return "Invalid Operation";
            }

            var userToDelete = await context.Users.FindAsync(userId);
            if (userToDelete == null)
            {
                return "Invalid Operation";
            }

            context.Talents.Remove(talentToDelete);
            context.Users.Remove(userToDelete);

            await context.SaveChangesAsync();
            return "Record Deleted!";
        }


    }
}
