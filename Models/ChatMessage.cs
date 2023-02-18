using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{

    public enum MessageStatus {
        Delivered,
        Read
    }

    [GraphQLDescription("User Chat Messages")]
    public class ChatMessage
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public Int64 ActiveChatId { get; set; } = default!;
        [Required]
        public Int64 UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public String Message { get; set; } = default!;
        public DateTime DateSent { get; set; } = default!;
        public MessageStatus Status { get; set; } = MessageStatus.Delivered;
    }
}