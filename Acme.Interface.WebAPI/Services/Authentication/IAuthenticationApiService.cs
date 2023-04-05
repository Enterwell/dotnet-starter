using Acme.Interface.WebAPI.Models.Authentication;

namespace Acme.Interface.WebAPI.Services.Authentication;

/// <summary>
/// Authentication Web API service interface.
/// </summary>
public interface IAuthenticationApiService
{
    /// <summary>
    /// Authenticates the user asynchronously.
    /// </summary>
    /// <param name="requestDto">Login request DTO.</param>
    /// <returns>
    /// A task returning <see cref="AuthenticationResponseDto"/>.
    /// </returns>
    Task<AuthenticationResponseDto> AuthenticateAsync(LoginRequestDto requestDto);
}