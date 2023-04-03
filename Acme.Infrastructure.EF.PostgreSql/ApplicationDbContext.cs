using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Acme.Infrastructure.EF.PostgreSql;

/// <summary>
/// Application DB context.
/// </summary>
/// <seealso cref="IdentityDbContext{TUser}"/>
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
}