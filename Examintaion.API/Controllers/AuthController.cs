using Microsoft.AspNetCore.Mvc;
using Template.Application.DTOs.Auth;
using Template.Application.Interfaces.Auth;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("login")]

        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO loginRequest)
        {
            var response = await _authServices.LoginAsync(loginRequest);
            if (response.IsSuccess)
            {
                SetRefreshTokenToCookie(response.Data.RefreshToken!, response.Data.RefreshTokenExpires);
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequest)
        {
            var response = await _authServices.RegisterAsync(registerRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authServices.GenerateNewJWTAndRefreshToken(refreshToken!);

            if (response != null)
            {
                SetRefreshTokenToCookie(response.RefreshToken!, response.RefreshTokenExpires);
                return Ok(response);
            }

            return BadRequest(new { message = "Invalid or expired refresh token." });


        }

        private void SetRefreshTokenToCookie(string refreshToken, DateTime expires)
        {
            CookieOptions options = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires
            };

            Response.Cookies.Append("refreshToken", refreshToken, options);

        }
    }
}
