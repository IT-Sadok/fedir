using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryBackend.Domain.Entities
{
    public class Dish
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public string MenuId { get; set; }

        [ForeignKey("MenuId")]
        public Menu? Menu { get; set; }
    }
}
