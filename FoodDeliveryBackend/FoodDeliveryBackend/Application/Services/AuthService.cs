using FoodDeliveryBackend.Application.Services.Interfaces;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Enums;
using FoodDeliveryBackend.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDeliveryBackend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequest loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return new AuthResponseDto { Success = false, ErrorMessage = "Invalid email or password." };

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded) return new AuthResponseDto { Success = false, ErrorMessage = "Invalid email or password." };

            var token = GenerateJwtToken(user);
            return new AuthResponseDto { Success = true, Token = token };
        }

        public async Task<AuthResponseDto> RegisterUserAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            try
            {
                await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            catch (Exception ex)
            {
                return new AuthResponseDto { Success = false, ErrorMessage = $"User registration failed. Details: {ex.Message}" };
            }

            return new AuthResponseDto { Success = true };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
