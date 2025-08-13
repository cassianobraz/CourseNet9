using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;

namespace JwtAuthDotNet9.Services;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request, CancellationToken ct);
    Task<TokenResponseDto?> LoginAsync(UserDto request, CancellationToken ct);
    Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request, CancellationToken ct);
}
