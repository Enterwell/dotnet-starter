using Acme.Infrastructure.EF.PostgreSql.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acme.Infrastructure.EF.PostgreSql.Configuration.EntityTypeConfigurations;

/// <summary>
/// Book database entity configuration.
/// </summary>
/// <seealso cref="IEntityTypeConfiguration{TEntity}"/>
public class BookConfiguration : IEntityTypeConfiguration<DbBook>
{
    /// <summary>
    /// Configures the entity.
    /// </summary>
    /// <param name="builder">The builder being used to construct the entities.</param>
    public void Configure(EntityTypeBuilder<DbBook> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).ValueGeneratedOnAdd();
    }
}