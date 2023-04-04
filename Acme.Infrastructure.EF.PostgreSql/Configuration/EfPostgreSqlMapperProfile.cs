using Acme.Core.Books;
using Acme.Core.Books.Interfaces;
using Acme.Core.Users;
using Acme.Core.Users.Interfaces;
using Acme.Infrastructure.EF.PostgreSql.Books;
using Acme.Infrastructure.EF.PostgreSql.Users;
using AutoMapper;

namespace Acme.Infrastructure.EF.PostgreSql.Configuration;

/// <summary>
/// Entity Framework PostgreSQL AutoMapper profile.
/// </summary>
public class EfPostgreSqlMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EfPostgreSqlMapperProfile"/> class.
    /// </summary>
    public EfPostgreSqlMapperProfile()
    {
        this.MapBookModels();
        this.MapApplicationUserModels();
    }

    /// <summary>
    /// Maps the book models.
    /// </summary>
    private void MapBookModels()
    {
        this.CreateMap<DbBook, Book>();
        this.CreateMap<DbBook, IBook>().As<Book>();
    }

    /// <summary>
    /// Maps the application user models.
    /// </summary>
    private void MapApplicationUserModels()
    {
        this.CreateMap<DbApplicationUser, ApplicationUser>();
        this.CreateMap<DbApplicationUser, IApplicationUser>().As<ApplicationUser>();
    }
}