using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("Invite Codes for celebrities")]
    public class InviteCode
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public Boolean Used { get; set; } = false;
        public Int64? UserId { get; set; } = default!;
        public User? User { get; set; } = default!;
    }
}