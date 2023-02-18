using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("User Talent Data")]
    public class Talent
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public Int64 Price { get; set; }
        [Required]
        public Boolean IsFeatured { get; set; } = false;
        [Required]
        public Boolean HasSale { get; set; } = false;
        [Required]
        public Int64 DeliveryTime { get; set; } = 24;
        public Int64 UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        [Required]
        public Int64 CategoryId { get; set; } = default!;
        public Category Category { get; set; } = default!;
    }
}