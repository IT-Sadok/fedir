using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryBackend.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = "";
    }
}