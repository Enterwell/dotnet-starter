using Acme.Interface.WebAPI.Controllers.v1.Base;
using Acme.Interface.WebAPI.Services.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Interface.WebAPI.Controllers.v1;

/// <summary>
/// Application management Web API controller.
/// </summary>
/// <seealso cref="BaseV1ApiController"/>
[Route("api/v1/application-management")]
public class ApplicationManagementController : BaseV1ApiController
{
    private readonly IManagementApiService managementApiService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationManagementController"/> class.
    /// </summary>
    /// <param name="managementApiService">Application management Web API service.</param>
    /// <exception cref="ArgumentNullException">
    /// managementApiService
    /// </exception>
    public ApplicationManagementController(IManagementApiService managementApiService)
    {
        this.managementApiService = managementApiService ?? throw new ArgumentNullException(nameof(managementApiService));
    }

    /// <summary>
    /// Asserts that migrations are up-to-date asynchronously.
    /// </summary>
    /// <returns>
    /// <c>True</c> if the migrations are up-to-date, <c>false</c> otherwise.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("assert-migrations")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> AssertMigrationsAsync() =>
        this.Ok(await this.managementApiService.AssertMigrationsAsync());

    /// <summary>
    /// Migrates the database asynchronously.
    /// </summary>
    /// <param name="targetMigration">(Optional) The target migration.</param>
    /// <returns>
    /// <c>True</c> if the database was migrated successfully, <c>false</c> otherwise.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("migrate")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> MigrateAsync(string? targetMigration = null) =>
        this.Ok(await this.managementApiService.MigrateAsync(targetMigration));
}