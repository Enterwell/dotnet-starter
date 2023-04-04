namespace Acme.Interface.WebAPI.Models.Paging;

/// <summary>
/// Paging data response DTO.
/// </summary>
/// <seealso cref="PagedRequestDto"/>
public class PagingDataResponseDto : PagedRequestDto
{
    /// <summary>
    /// Gets or sets the total items count.
    /// </summary>
    public int TotalItems { get; set; }
}