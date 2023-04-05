using Microsoft.EntityFrameworkCore;

namespace Acme.Tests.Helpers;

/// <summary>
/// DbSet helper extensions.
/// </summary>
public static class DbSetExtensions
{
    /// <summary>
    /// A <see cref="DbSet{TEntity}"/> extension method that marks all of its entities for deletion.
    /// </summary>
    /// <typeparam name="T">Entity type parameter.</typeparam>
    /// <param name="set">The <see cref="DbSet{TEntity}"/> to act on.</param>
    public static void RemoveAll<T>(this DbSet<T> set) where T : class
    {
        set.RemoveRange(set.IgnoreQueryFilters());
    }
}