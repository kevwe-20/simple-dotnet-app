using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("User Notification")]
    public class Notification
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public DateTime DateSent { get; set; } = default!;
        [Required]
        public string Message { get; set; } = default!;
        [Required]
        public MessageStatus Status { get; set; } = MessageStatus.Delivered;
        [Required]
        public Int64 UserId { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}