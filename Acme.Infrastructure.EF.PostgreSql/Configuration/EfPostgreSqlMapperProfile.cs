using Acme.Core.Users;
using Acme.Core.Users.Interfaces;
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
        this.MapApplicationUserModels();
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