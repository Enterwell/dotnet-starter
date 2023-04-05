using Microsoft.AspNetCore.Mvc;

namespace Acme.Interface.WebAPI.Controllers.v1.Base;

/// <summary>
/// Base v1 API controller that every other controller extends.
/// Used to remove repeating <see cref="ControllerBase"/> inheritance and API controller annotations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class BaseV1ApiController : ControllerBase
{
}