using Acme.Core.Authentication.Interfaces;
using Acme.Core.Books;
using Acme.Core.Books.Interfaces;
using Acme.Core.Paging;
using Acme.Core.Paging.Interfaces;
using Acme.Interface.WebAPI.Models.Authentication;
using Acme.Interface.WebAPI.Models.Books;
using Acme.Interface.WebAPI.Models.Paging;
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
        this.MapPagingModels();
        this.MapBookModels();
    }

    /// <summary>
    /// Maps the authentication models.
    /// </summary>
    private void MapAuthenticationModels()
    {
        this.CreateMap<IAuthenticationResponse, AuthenticationResponseDto>();
        this.CreateMap<IToken, TokenDto>();
    }

    /// <summary>
    /// Maps the paging models.
    /// </summary>
    private void MapPagingModels()
    {
        this.CreateMap<PagedRequestDto, PagedRequest>();
        this.CreateMap<PagedRequestDto, IPagedRequest>().As<PagedRequest>();
    }

    /// <summary>
    /// Maps the book models.
    /// </summary>
    private void MapBookModels()
    {
        this.CreateMap<IBook, BookDto>();

        this.CreateMap<BookCreateDto, BookCreate>();
        this.CreateMap<BookCreateDto, IBookCreate>().As<BookCreate>();

        this.CreateMap<BookUpdateDto, BookUpdate>();
        this.CreateMap<BookUpdateDto, IBookUpdate>().As<BookUpdate>();
    }
}