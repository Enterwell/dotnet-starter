using Acme.Core.Authentication.Interfaces;
using Acme.Core.CodeExtensions;
using Acme.Core.Exceptions;
using Acme.Interface.WebAPI.Models.Authentication;
using AutoMapper;
using Enterwell.Exceptions;

namespace Acme.Interface.WebAPI.Services.Authentication;

/// <summary>
/// Authentication Web API service class.
/// </summary>
/// <seealso cref="IAuthenticationApiService"/>
public class AuthenticationApiService : IAuthenticationApiService
{
    private readonly IAuthenticationService authenticationService;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationApiService"/> class.
    /// </summary>
    /// <param name="authenticationService">Authentication service.</param>
    /// <param name="mapper">The mapper.</param>
    /// <exception cref="ArgumentNullException">
    /// authenticationService
    /// or
    /// mapper
    /// </exception>
    public AuthenticationApiService(IAuthenticationService authenticationService, IMapper mapper)
    {
        this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Authenticates the user asynchronously.
    /// </summary>
    /// <param name="requestDto">Login request DTO.</param>
    /// <returns>
    /// A task returning <see cref="AuthenticationResponseDto"/>.
    /// </returns>
    public async Task<AuthenticationResponseDto> AuthenticateAsync(LoginRequestDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Email) || string.IsNullOrWhiteSpace(requestDto.Password))
        {
            throw new EnterwellValidationException("Username or password is not valid.", null, ErrorCodes.Authentication.InvalidEmailOrPassword);
        }

        var authResponse = await this.authenticationService.AuthenticateAsync(requestDto.Email, requestDto.Password);
        if (authResponse == null)
        {
            throw new EnterwellValidationException("Username or password is not valid.", null, ErrorCodes.Authentication.InvalidEmailOrPassword);
        }

        return authResponse.MapTo<AuthenticationResponseDto>(this.mapper);
    }
}