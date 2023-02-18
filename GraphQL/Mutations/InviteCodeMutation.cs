namespace Ampifan.GraphQL.Mutations
{
    using HotChocolate;
    using HotChocolate.Types;
    using Ampifan.GraphQL.Inputs.InviteCode;
    using Ampifan.Models;

    [ExtendObjectType("Mutation")]
    public class InviteCodeMutation
    {
        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddInviteCodePayload> AddInviteCode(AddInviteCodeInput input, [ScopedService] AppDBContext context)
        {
            var inviteCode = new InviteCode
            {
                Code = input.Code,
                Used = false,
            };
            context.InviteCodes.Add(inviteCode);
            await context.SaveChangesAsync();
            return new AddInviteCodePayload(inviteCode);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddInviteCodePayload> UpdateInviteCode(UpdateInviteCodeInput input, [ScopedService] AppDBContext context)
        {
            var updatedInviteCode = new InviteCode
            {
                Id = input.Id,
                Code = input.Code,
                Used = input.Used
            };
            context.InviteCodes.Update(updatedInviteCode);
            await context.SaveChangesAsync();
            return new AddInviteCodePayload(updatedInviteCode);
        }

        [UseDbContext(typeof(AppDBContext))]
        public async Task<String> DeleteInviteCode(Int64 id, [ScopedService] AppDBContext context)
        {
            var inviteCodeToDelete = await context.InviteCodes.FindAsync(id);
            if (inviteCodeToDelete == null)
            {
                return "Invalid Operation";
            }

            context.InviteCodes.Remove(inviteCodeToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }


    }
}
