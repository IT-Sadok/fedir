using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryBackend.Domain.DTO
{
    public class RegisterModel
    {
        [Required] public string FullName { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required][MinLength(6)] public string Password { get; set; }
    }
}
