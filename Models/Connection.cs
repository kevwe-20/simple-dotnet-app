using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("User Connections - Details")]
    public class Connection
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public Int64 UserConnectionId { get; set; } = default!;
        [Required]
        public DateTime DateConnected { get; set; } = default!;
        [Required]
        public Boolean isFollowing { get; set; } = false!;
        [Required]
        public Int64 UserId { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}