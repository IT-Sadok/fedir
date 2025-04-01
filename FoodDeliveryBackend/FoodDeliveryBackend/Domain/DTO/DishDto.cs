namespace FoodDeliveryBackend.Domain.DTO
{
    public class DishDto
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string MenuId { get; set; }
    }
}
