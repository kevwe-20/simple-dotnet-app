namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.User;
    using Ampifan.Models;

    [ExtendObjectType("Mutation")]
    public class UserMutation
    {
        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddUserPayload> AddUser(AddUserInput input, [ScopedService] AppDBContext context)
        {
            // Check if user with email exists
            var userExist = context.Users.Where(c => c.Email == input.Email).FirstOrDefault();
            if (userExist != null)
            {
                throw new GraphQLException("Email already exists");
            }

            var user = new User
            {
                Name = input.Name,
                Email = input.Email,
                Bio = input.Bio,
                PhotoUrl = input.PhotoUrl,
                PushToken = input.PushToken,
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new AddUserPayload(user);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddUserPayload> UpdateUser(UpdateUserInput input, [ScopedService] AppDBContext context)
        {
            var updatedUser = new User
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                Bio = input.Bio,
                PhotoUrl = input.PhotoUrl,
                PushToken = input.PushToken,
            };
            context.Users.Update(updatedUser);
            await context.SaveChangesAsync();
            return new AddUserPayload(updatedUser);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteUser(Int64 id, [ScopedService] AppDBContext context)
        {
            var userToDelete = await context.Users.FindAsync(id);
            if (userToDelete == null)
            {
                return "Invalid Operation";
            }

            context.Users.Remove(userToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }


    }
}
