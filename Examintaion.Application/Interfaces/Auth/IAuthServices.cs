using Template.API.Response;
using Template.Application.DTOs.Auth;

namespace Template.Application.Interfaces.Auth
{
    public interface IAuthServices
    {
        Task<ApiResponse<LoginResponseDTO>> LoginAsync(LoginRequestDTO loginRequest);

        Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterRequestDto registerRequest);

        Task<LoginResponseDTO> GenerateNewJWTAndRefreshToken(string token);

    }
}
