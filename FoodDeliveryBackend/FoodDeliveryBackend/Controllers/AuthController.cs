using FoodDeliveryBackend.Domain.DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using FoodDeliveryBackend.Application.Services.Interfaces;

namespace FoodDeliveryBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User Registration
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(model);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        /// <summary>
        /// User Login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var result = await _authService.LoginAsync(model);
            return Ok(result);
        }
    }
}
