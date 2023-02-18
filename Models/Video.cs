using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("Uploaded User Video")]
    public class Video
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public Boolean Private { get; set; } = false;
        [Required]
        public string Url { get; set; } = default!;
        [Required]
        public Int64 UserId { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}