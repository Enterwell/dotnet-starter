using Acme.Application.Tests.Mocks;
using Acme.Core.Books;
using Acme.Core.Books.Interfaces;
using Acme.Core.Paging.Enums;
using Acme.Infrastructure.EF.PostgreSql.Paging;
using Acme.Interface.WebAPI.Models.Books;
using Acme.Interface.WebAPI.Models.Paging;
using Acme.Interface.WebAPI.Services.Books;
using Acme.Interface.WebAPI.Tests.ServiceRegistrars;
using Enterwell.Exceptions;
using FluentAssertions;
using Moq;

namespace Acme.Interface.WebAPI.Tests.Services;

/// <summary>
/// Books Web API service tests class.
/// </summary>
/// <seealso cref="ApiUnitTestBase{TMockedServices}"/>
/// <seealso cref="IDisposable"/>
public class BooksApiServiceTests : IClassFixture<ApiUnitTestBase<BooksApiServiceTestsMocks>>, IDisposable
{
    private readonly IBooksApiService booksApiService;

    private readonly BooksServiceMock booksServiceMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="BooksApiServiceTests"/> class.
    /// </summary>
    /// <param name="testBase">Api unit test base.</param>
    public BooksApiServiceTests(ApiUnitTestBase<BooksApiServiceTestsMocks> testBase)
    {
        this.booksApiService = testBase.Resolve<IBooksApiService>();

        this.booksServiceMock = testBase.MockedServices.BooksServiceMock;
    }

    /// <summary>
    /// Tests that the <see cref="IBooksApiService.GetByIdAsync"/> throws an <see cref="EnterwellEntityNotFoundException"/> when trying to get
    /// a book that does not exist.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithNonExistingBook_ThrowsAnException()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();

        // Act.
        Func<Task> act = () => this.booksApiService.GetByIdAsync(testId);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellEntityNotFoundException>();

        this.booksServiceMock.VerifyGetByIdAsync(testId, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksApiService.GetByIdAsync"/> returns <see cref="BookDto"/> correctly
    /// when valid params are passed in.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithValidParams_ReturnsCorrectly()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();
        var bookToReturn = new Book
        {
            Id = testId,
            Author = "TestAuthor",
            Name = "TestName"
        };

        var expectedResult = new BookDto
        {
            Id = testId,
            Author = bookToReturn.Author,
            Name = bookToReturn.Name
        };

        this.booksServiceMock.SetupGetByIdAsync(testId, bookToReturn);

        // Act.
        var result = await this.booksApiService.GetByIdAsync(testId);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

        this.booksServiceMock.VerifyGetByIdAsync(testId, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksApiService.CreateAsync"/> calls the service and returns correctly.
    /// </summary>
    [Fact]
    public async Task CreateAsync_CallsService_ReturnsCorrectly()
    {
        // Arrange.
        var createDto = new BookCreateDto
        {
            Author = "TestAuthor",
            Name = "TestName"
        };
        var bookCreate = new BookCreate
        {
            Author = createDto.Author,
            Name = createDto.Name
        };
        var bookToReturn = new Book
        {
            Author = createDto.Author,
            Name = createDto.Name
        };
        var expectedResult = new BookDto
        {
            Id = It.IsAny<string>(),
            Author = createDto.Author,
            Name = createDto.Name
        };

        this.booksServiceMock.SetupCreateAsync(bookToReturn);

        // Act.
        var result = await this.booksApiService.CreateAsync(createDto);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

        this.booksServiceMock.VerifyCreateAsync(bookCreate.Name, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksApiService.UpdateAsync"/> calls the service correctly.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_CallsServiceCorrectly()
    {
        // Arrange.
        var updateDto = new BookUpdateDto
        {
            Id = Guid.NewGuid().ToString(),
            Author = "TestAuthor",
            Name = "TestName"
        };
        var bookUpdate = new BookUpdate
        {
            Id = updateDto.Id,
            Author = updateDto.Author,
            Name = updateDto.Name
        };

        // Act.
        Func<Task> act = () => this.booksApiService.UpdateAsync(updateDto);

        // Assert.
        await act.Should().NotThrowAsync();

        this.booksServiceMock.VerifyUpdateAsync(bookUpdate.Id, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksApiService.DeleteAsync"/> calls the service correctly.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_CallsServiceCorrectly()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();

        // Act.
        await this.booksApiService.DeleteAsync(testId);

        // Assert.
        this.booksServiceMock.VerifyDeleteAsync(testId, Times.Once());
    }

    /// <summary>
    /// Dispose pattern method called after every test to cleanup. Used to reset mock states.
    /// </summary>
    public void Dispose()
    {
        this.booksServiceMock.Reset();
    }
}