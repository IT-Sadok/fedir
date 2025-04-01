using FoodDeliveryBackend.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FoodDeliveryBackend.Domain.Entities;

namespace FoodDeliveryBackend.Domain.DTO
{
    public class MenuDto
    {
        public string Id { get; set; }
        public string RestaurantId { get; set; } = string.Empty;
        public ICollection<DishDto> Dishes { get; set; } = new List<DishDto>(); // One-to-Many
    }
}
