using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("Application Analytics")]
    public class Analytic
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public string Data { get; set; } = default!;
        public Int64 UserId { get; set; } = default!;
    }
}