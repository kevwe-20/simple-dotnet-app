using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("Registered Users")]
    public class User
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string Bio { get; set; } = default!;
        [Required]
        public string PhotoUrl { get; set; } = default!;
        public string PushToken { get; set; } = default!;

        public Connection Connection { get; set; } = default!;
        public InviteCode InviteCode { get; set; } = default!;
        public Talent Talent { get; set; } = default!;
        public List<ActiveChat> ActiveChats { get; } = new();
        public List<ChatMessage> ChatMessages { get; } = new();
        public List<Video> Videos { get; } = default!;

    }
}