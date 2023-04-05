using Acme.Application.Tests.ServiceRegistrars;
using Acme.Core.Books;
using Acme.Core.Books.Interfaces;
using Acme.Core.Paging;
using Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;
using FluentAssertions;
using Moq;

namespace Acme.Application.Tests.Services;

/// <summary>
/// Books service tests class.
/// </summary>
/// <seealso cref="ApplicationUnitTestBase{TMockedServices}"/>
/// <seealso cref="BooksServiceTestsMocks"/>
/// <seealso cref="IDisposable"/>
public class BooksServiceTests : IClassFixture<ApplicationUnitTestBase<BooksServiceTestsMocks>>, IDisposable
{
    private readonly IBooksService booksService;

    private readonly BooksRepositoryMock booksRepositoryMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="BooksServiceTests"/> class.
    /// </summary>
    /// <param name="testBase">Application unit test base.</param>
    public BooksServiceTests(ApplicationUnitTestBase<BooksServiceTestsMocks> testBase)
    {
        this.booksService = testBase.Resolve<IBooksService>();

        this.booksRepositoryMock = testBase.MockedServices.BooksRepositoryMock;
    }

    /// <summary>
    /// Tests that the <see cref="IBooksService.GetAllPaged"/> calls the repository.
    /// </summary>
    [Fact]
    public void GetAllPaged_CallsRepo()
    {
        // Arrange.
        var pagedRequest = new PagedRequest
        {
            Page = 0,
            PageSize = 10
        };

        // Act.
        var result = this.booksService.GetAllPaged(pagedRequest);

        // Assert.
        result.Should().BeNull();

        this.booksRepositoryMock.VerifyGetAllPaged(pagedRequest, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksService.GetByIdAsync"/> calls the repository and returns correctly.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_CallsRepo_ReturnsCorrectly()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();

        var bookToReturn = new Book { Id = testId, Author = "Author" };

        this.booksRepositoryMock.SetupGetByIdAsync(testId, bookToReturn);

        // Act.
        var result = await this.booksService.GetByIdAsync(testId);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bookToReturn);

        this.booksRepositoryMock.VerifyGetByIdAsync(testId, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksService.CreateAsync"/> calls the repository and returns correctly.
    /// </summary>
    [Fact]
    public async Task CreateAsync_CallsRepo_ReturnsCorrectly()
    {
        // Arrange.
        var bookCreate = new BookCreate
        {
            Name = "TestName",
            Author = "Author"
        };

        var bookToReturn = new Book
        {
            Author = bookCreate.Author,
            Name = bookCreate.Name
        };

        this.booksRepositoryMock.SetupCreateAsync(bookToReturn);

        // Act.
        var result = await this.booksService.CreateAsync(bookCreate);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bookToReturn);

        this.booksRepositoryMock.VerifyCreateAsync(bookCreate, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksService.UpdateAsync"/> calls the repository.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_CallsRepo()
    {
        // Arrange.
        var bookUpdate = new BookUpdate
        {
            Name = "TestName",
            Author = "Author"
        };

        // Act.
        await this.booksService.UpdateAsync(bookUpdate);

        // Assert.
        this.booksRepositoryMock.VerifyUpdateAsync(bookUpdate, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IBooksService.DeleteAsync"/> calls the repository.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_CallsRepo()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();

        // Act.
        await this.booksService.DeleteAsync(testId);

        // Assert.
        this.booksRepositoryMock.VerifyDeleteAsync(testId, Times.Once());
    }

    /// <summary>
    /// Dispose pattern method called after every test to cleanup. Used to reset mock states.
    /// </summary>
    public void Dispose()
    {
        this.booksRepositoryMock.Reset();
    }
}