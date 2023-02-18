using System.ComponentModel.DataAnnotations;

namespace Ampifan.Models
{
    [GraphQLDescription("Talent Categories for celebrities")]
    public class Category
    {
        [Key]
        public Int64 Id { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        public Boolean Active { get; set; }
    
        public List<Talent> Talents { get; } = new(); 
    }
}