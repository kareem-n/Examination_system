using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Examination.Domain.Models;
using Examination.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Template.API.Response;
using Template.Application.DTOs.Auth;
using Template.Application.Interfaces.Auth;


namespace Template.Application.Services.Auth
{
    internal class AuthService : IAuthServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, AppDbContext appDbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            this.appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task<ApiResponse<LoginResponseDTO>> LoginAsync(LoginRequestDTO loginRequest)
        {

            var user = _userManager.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefault(u => u.Email == loginRequest.Email)
                ;

            if (user == null)
            {
                return ApiResponse<LoginResponseDTO>.Error(404, "Invalid Login Attempt");
            }

            var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);


            if (!result)
            {
                return ApiResponse<LoginResponseDTO>.Error(404, "Invalid Login Attempt");
            }

            var jti = Guid.NewGuid().ToString();
            var token = await GenerateToken(user, jti);

            RefreshToken refresh = null!;

            if (user.RefreshTokens.Any(rt => rt.IsActive))
            {
                refresh = user.RefreshTokens.FirstOrDefault(rt => rt.IsActive)!;
            }
            else
            {
                refresh = GenerateRefreshToken(user, jti);
                appDbContext.RefreshTokens.Add(refresh);
                await appDbContext.SaveChangesAsync();
            }


            var response = new LoginResponseDTO
            {
                Id = user.Id.ToString(),
                AccessToken = token,
                RefreshToken = refresh.Token,
                RefreshTokenExpires = refresh.Expiration,
            };

            return ApiResponse<LoginResponseDTO>.Success(200, "Login Successful", response);

        }


        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterRequestDto registerrequestDto)
        {
            if (await _userManager.FindByEmailAsync(registerrequestDto.Email) == null)
            {
                var user = new AppUser()
                {
                    UserName = registerrequestDto.Username,
                    Email = registerrequestDto.Email,
                    Id = Guid.NewGuid().ToString() // Ensure the Id is initialized with a unique value  
                };

                var result = await _userManager.CreateAsync(user, registerrequestDto.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(error => new { error.Code, error.Description });
                    return ApiResponse<RegisterResponseDto>.Error(400, "Registration Failed", errors.ToArray());
                }

                await _userManager.AddToRoleAsync(user, "student");
                var response = new RegisterResponseDto
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                };
                return ApiResponse<RegisterResponseDto>.Success(201, "Registration Successful", response);
            }
            var error = new { Code = "EmailExists", Description = "Email Already Exists" };
            return ApiResponse<RegisterResponseDto>.Error(400, "Registration Failed", new[] { error });
        }

        private async Task<string> GenerateToken(AppUser user, string jti)
        {
            List<Claim> claims =
            [
                new Claim( JwtRegisteredClaimNames.Jti , jti  ) ,
                new Claim( JwtRegisteredClaimNames.Sub , user.UserName! ) ,
                new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            ];

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;

        }

        private string ComputeSha256Hash(string input)
        {
            using var sha = SHA256.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        private RefreshToken GenerateRefreshToken(AppUser user, string jti)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrEmpty(jti))
            {
                throw new ArgumentException("JTI cannot be null or empty", nameof(jti));
            }

            // Generate a new refresh token
            // Ensure the token is unique by using a new GUID
            var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var tokenHash = ComputeSha256Hash(rawToken);


            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                Token = tokenHash,
                Expiration = DateTime.UtcNow.AddDays(7),
                UserId = user.Id,
            };


            return refreshToken;

        }

        public async Task<LoginResponseDTO> GenerateNewJWTAndRefreshToken(string token)
        {
            var user = await _userManager.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new ArgumentException("Invalid refresh token", nameof(token));
            }

            var refres = user.RefreshTokens;
            var refreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == token && t.IsActive);

            if (refreshToken == null)
            {
                throw new ArgumentException("Invalid or expired refresh token", nameof(token));
            }

            // Generate a new JWT token
            var jti = Guid.NewGuid().ToString();
            var newJwtToken = await GenerateToken(user, jti);
            // Generate a new refresh token
            var newRefreshToken = GenerateRefreshToken(user, jti);
            // Remove the old refresh token

            refreshToken.RevokedAt = DateTime.UtcNow;

            // Add the new refresh token to the database
            appDbContext.RefreshTokens.Add(newRefreshToken);
            await appDbContext.SaveChangesAsync();
            return new LoginResponseDTO
            {
                Id = user.Id.ToString(),
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpires = newRefreshToken.Expiration
            };

        }
    }
}
