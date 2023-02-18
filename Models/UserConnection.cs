using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("User Connections - Followers/Following")]
    public class UserConnection
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        public Int64 UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public List<Connection> Connections { get; set; } = default!;
    }
}