using Acme.Infrastructure.EF.PostgreSql.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Acme.Infrastructure.EF.PostgreSql;

/// <summary>
/// Application DB context.
/// </summary>
/// <seealso cref="IdentityDbContext{TUser}"/>
public class ApplicationDbContext : IdentityDbContext<DbApplicationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">Context options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}