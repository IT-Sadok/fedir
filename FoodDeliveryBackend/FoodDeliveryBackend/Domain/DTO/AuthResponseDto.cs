namespace FoodDeliveryBackend.Domain.DTO
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
