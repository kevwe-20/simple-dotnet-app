using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("Current Active chats for users")]
    public class ActiveChat
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public Int64 UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public List<ChatMessage> ChatMessages { get; set; } = default!;
    }
}