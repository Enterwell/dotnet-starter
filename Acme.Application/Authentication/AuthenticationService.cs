using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Acme.Core.Authentication;
using Acme.Core.Authentication.Interfaces;
using Acme.Core.Settings.Interfaces;
using Acme.Core.Users.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Acme.Application.Authentication;

/// <summary>
/// Authentication service class.
/// </summary>
/// <seealso cref="IAuthenticationService"/>
public class AuthenticationService : IAuthenticationService
{
    private readonly IApplicationUsersRepository applicationUserRepository;
    private readonly ITokenConfiguration tokenConfiguration;
    private readonly ILogger<AuthenticationService> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="applicationSettingsProvider">Application settings provider.</param>
    /// <param name="applicationUserRepository">Application user repository.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">
    /// applicationSettingsProvider
    /// or
    /// applicationUserRepository
    /// or
    /// logger
    /// </exception>
    public AuthenticationService(IApplicationSettingsProvider applicationSettingsProvider, IApplicationUsersRepository applicationUserRepository, ILogger<AuthenticationService> logger)
    {
        this.applicationUserRepository = applicationUserRepository ?? throw new ArgumentNullException(nameof(applicationUserRepository));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        ArgumentNullException.ThrowIfNull(applicationSettingsProvider);
        this.tokenConfiguration = applicationSettingsProvider.TokenConfiguration;
    }

    /// <summary>
    /// Authenticates the user asynchronously.
    /// </summary>
    /// <param name="email">User's email address.</param>
    /// <param name="password">User's password.</param>
    /// <returns>
    /// An <see cref="IAuthenticationResponse"/> instance or <c>null</c> if authentication failed.
    /// </returns>
    public async Task<IAuthenticationResponse?> AuthenticateAsync(string email, string password)
    {
        // Try to get user from the repository
        var user = await this.applicationUserRepository.GetByEmailAndPasswordAsync(email, password);

        // Check if found
        if (user == null) return null;

        // Generate JWT token
        var token = this.GenerateJwtToken(user);

        this.logger.LogInformation($"Application Login: UserId: {user.Id}, username: {user.Username}");

        return new AuthenticationResponse(user.Id, user.Username, token);
    }

    /// <summary>
    /// Generates the JWT token and creates the <see cref="IToken"/> instance.
    /// </summary>
    /// <param name="user">User for which to generate the JWT token.</param>
    /// <returns>
    /// <see cref="IToken"/> instance.
    /// </returns>
    private IToken GenerateJwtToken(IApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        var key = Encoding.ASCII.GetBytes(this.tokenConfiguration.JwtSecret);
        var created = DateTime.UtcNow;
        var expires = created.AddSeconds(this.tokenConfiguration.JwtExpirationSeconds);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new Token(tokenString, created, expires);
    }
}