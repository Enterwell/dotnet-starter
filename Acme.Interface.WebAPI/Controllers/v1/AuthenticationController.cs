using Acme.Interface.WebAPI.Controllers.v1.Base;
using Acme.Interface.WebAPI.Models.Authentication;
using Acme.Interface.WebAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Interface.WebAPI.Controllers.v1;

/// <summary>
/// Authentication Web API controller.
/// </summary>
/// <seealso cref="BaseV1ApiController"/>
[AllowAnonymous]
public class AuthenticationController : BaseV1ApiController
{
    private readonly IAuthenticationApiService authenticationApiService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    /// <param name="authenticationApiService">Authentication Web API service.</param>
    /// <exception cref="ArgumentNullException">
    /// authenticationApiService
    /// </exception>
    public AuthenticationController(IAuthenticationApiService authenticationApiService)
    {
        this.authenticationApiService = authenticationApiService ?? throw new ArgumentNullException(nameof(authenticationApiService));
    }

    /// <summary>
    /// Authenticates (logs in) the user asynchronously.
    /// </summary>
    /// <param name="requestDto">Login request DTO.</param>
    /// <returns>
    /// A task returning <see cref="AuthenticationResponseDto"/>.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto requestDto) =>
        this.Ok(await this.authenticationApiService.AuthenticateAsync(requestDto));
}