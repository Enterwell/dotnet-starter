using Acme.Core.Authentication.Interfaces;
using Acme.Interface.WebAPI.Models.Authentication;
using AutoMapper;

namespace Acme.Interface.WebAPI.Configuration;

/// <summary>
/// Web API AutoMapper profile.
/// </summary>
public class ApiMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiMapperProfile"/> class.
    /// </summary>
    public ApiMapperProfile()
    {
        this.MapAuthenticationModels();
    }

    /// <summary>
    /// Maps the authentication models.
    /// </summary>
    private void MapAuthenticationModels()
    {
        this.CreateMap<IAuthenticationResponse, AuthenticationResponseDto>();
        this.CreateMap<IToken, TokenDto>();
    }
}