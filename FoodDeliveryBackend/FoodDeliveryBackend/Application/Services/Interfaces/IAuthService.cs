using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Enums;
using FoodDeliveryBackend.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequest model);
        Task<AuthResponseDto> RegisterUserAsync(RegisterModel model);
    }
}
