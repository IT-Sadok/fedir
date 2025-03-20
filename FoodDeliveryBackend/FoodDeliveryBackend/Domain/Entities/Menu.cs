using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryBackend.Domain.Entities
{
    public class Menu
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant? Restaurant { get; set; }

        public ICollection<Dish> Dishes { get; set; } = new List<Dish>(); // One-to-Many
    }
}
