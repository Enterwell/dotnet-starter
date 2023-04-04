using Acme.Infrastructure.EF.PostgreSql.Books;
using Acme.Infrastructure.EF.PostgreSql.Configuration.EntityTypeConfigurations;
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

    /// <summary>
    /// Gets or sets the books.
    /// </summary>
    public virtual DbSet<DbBook> Books { get; set; }

    /// <summary>
    /// Configures the schema needed.
    /// </summary>
    /// <param name="builder">The builder being used to construct the entities.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new BookConfiguration());
    }
}