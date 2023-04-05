using Acme.Core.Books;
using Acme.Core.Books.Interfaces;
using Acme.Core.Paging.Interfaces;
using Acme.Core.Paging;
using Acme.Tests;
using Acme.Tests.Helpers;
using Enterwell.Exceptions;
using FluentAssertions;

namespace Acme.Infrastructure.EF.PostgreSql.Tests.Repositories;

/// <summary>
/// Entity Framework PostgreSQL books repository tests class.
/// </summary>
/// <seealso cref="IntegrationTestBase"/>
/// <seealso cref="IDisposable"/>
public class EfPostgreSqlBooksRepositoryTests : IClassFixture<IntegrationTestBase>, IDisposable
{
    private readonly IBooksRepository booksRepository;

    private readonly IntegrationTestBase testBase;
    private readonly ApplicationDbContext context;
    private readonly DatabaseEntityFactory entityFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfPostgreSqlBooksRepositoryTests"/> class.
    /// </summary>
    /// <param name="testBase">Integration test base.</param>
    public EfPostgreSqlBooksRepositoryTests(IntegrationTestBase testBase)
    {
        this.booksRepository = testBase.Resolve<IBooksRepository>();

        this.testBase = testBase;
        this.context = testBase.Context;
        this.entityFactory = testBase.EntityFactory;
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.GetAllPaged"/> throws an <see cref="ArgumentNullException"/> if the
    /// <see cref="IPagedRequest"/> argument is <c>null</c>.
    /// </summary>
    [Fact]
    public void GetAllPaged_NullPagedRequest_ThrowsAnException()
    {
        // Arrange.
        IPagedRequest? pagedRequest = null;

        // Act.
        var act = () => this.booksRepository.GetAllPaged(pagedRequest!);

        // Assert.
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.GetAllPaged"/> repository method returns empty if the collection is empty.
    /// </summary>
    [Fact]
    public async Task GetAllPaged_EmptyCollection_ReturnsEmpty()
    {
        // Arrange.
        var pagedRequest = new PagedRequest
        {
            Page = 0,
            PageSize = 20,
        };

        // Act.
        var pagedResult = this.booksRepository.GetAllPaged(pagedRequest);
        var result = await pagedResult.GetPageAsync(pagedRequest.Page, pagedRequest.PageSize);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.GetAllPaged"/> repository method returns correctly.
    /// </summary>
    [Fact]
    public async Task GetAllPaged_NotEmpty_ReturnsCorrect()
    {
        // Arrange.
        var booksCount = 20;

        for (var i = 0; i < booksCount; i++)
        {
            await this.entityFactory.CreateBookAsync($"Book {i + 1}");
        }

        var pagedRequest = new PagedRequest
        {
            Page = 0,
            PageSize = 10
        };

        // Act.
        var pagedResult = this.booksRepository.GetAllPaged(pagedRequest);
        var result = await pagedResult.GetPageAsync(pagedRequest.Page, pagedRequest.PageSize);

        // Assert.
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(pagedRequest.PageSize);
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.GetByIdAsync"/> returns <c>null</c> if the book with the
    /// given identifier does not exist.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithNonExistingBook_ReturnsNull()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();

        // Act.
        var fetchedBook = await this.booksRepository.GetByIdAsync(testId);

        // Assert.
        fetchedBook.Should().BeNull();
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.GetByIdAsync"/> returns correct book when valid identifier is passed in.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithValidParams_ReturnsCorrectly()
    {
        // Arrange.
        var existingBook = await this.entityFactory.CreateBookAsync("Book 1");

        // Act.
        var fetchedBook = await this.booksRepository.GetByIdAsync(existingBook.Id!);

        // Assert.
        fetchedBook.Should().NotBeNull();
        fetchedBook.Should().BeEquivalentTo(existingBook);
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.CreateAsync"/> throws an <see cref="ArgumentNullException"/>
    /// when the create model is <c>null</c>.
    /// </summary>
    [Fact]
    public async Task CreateAsync_WithNullCreateModel_ThrowsAnException()
    {
        // Arrange.
        IBookCreate? bookCreate = null;

        // Act.
        Func<Task> act = () => this.booksRepository.CreateAsync(bookCreate!);

        // Assert.
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.CreateAsync"/> creates the book correctly when
    /// provided with valid param.
    /// </summary>
    [Fact]
    public async Task CreateAsync_WithValidParams_CreatesOk()
    {
        // Arrange.
        var createModel = new BookCreate
        {
            Author = "Book author",
            Category = "Book category",
            Name = "Book name",
            Price = 20
        };

        // Act.
        var createdBook = await this.booksRepository.CreateAsync(createModel);

        // Assert.
        createdBook.Should().NotBeNull();
        createdBook.Id.Should().NotBeNullOrWhiteSpace();

        var bookFromDb = await this.context.Books.FindAsync(createdBook.Id);

        bookFromDb.Should().NotBeNull();
        bookFromDb.Should().BeEquivalentTo(createdBook);
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.UpdateAsync"/> throws an <see cref="ArgumentNullException"/>
    /// when the update model is <c>null</c>.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_WithNullUpdateModel_ThrowsAnException()
    {
        // Arrange.
        IBookUpdate? bookUpdate = null;

        // Act.
        Func<Task> act = () => this.booksRepository.UpdateAsync(bookUpdate!);

        // Assert.
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.UpdateAsync"/> throws an <see cref="EnterwellEntityNotFoundException"/>
    /// when trying to update a book that does not exist.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_WithNonExistingBook_ThrowsAnException()
    {
        // Arrange.
        var bookUpdate = new BookUpdate
        {
            Id = Guid.NewGuid().ToString()
        };

        // Act.
        Func<Task> act = () => this.booksRepository.UpdateAsync(bookUpdate);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellEntityNotFoundException>();
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.UpdateAsync"/> updates correctly when the valid params are passed in.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_WithValidParams_UpdatesCorrectly()
    {
        // Arrange.
        var bookToUpdate = await this.entityFactory.CreateBookAsync("Initial Name");

        var bookUpdate = new BookUpdate
        {
            Id = bookToUpdate.Id,
            Name = "Updated name",
            Author = "Updated author",
            Category = "Updated category",
            Price = 2
        };

        // Act.
        await this.booksRepository.UpdateAsync(bookUpdate);

        var bookFromDb = await this.context.Books.FindAsync(bookUpdate.Id);

        // Assert.
        bookFromDb.Should().NotBeNull();
        bookFromDb.Should().BeEquivalentTo(bookUpdate);
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.DeleteAsync"/> throws an <see cref="EnterwellEntityNotFoundException"/>
    /// when trying to delete a book that does not exist.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_WithNonExistingBook_ThrowsAnException()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();

        // Act.
        Func<Task> act = () => this.booksRepository.DeleteAsync(testId);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellEntityNotFoundException>();
    }

    /// <summary>
    /// Tests that the <see cref="IBooksRepository.DeleteAsync"/> deletes the book correctly when provided with
    /// the valid params.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_WithValidParams_DeletesCorrectly()
    {
        // Arrange.
        var existingBook = await this.entityFactory.CreateBookAsync("Book 1");

        // Act.
        await this.booksRepository.DeleteAsync(existingBook.Id);

        var bookFromDb = await this.context.Books.FindAsync(existingBook.Id);

        // Assert.
        bookFromDb.Should().BeNull();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.testBase.PerformActionWithEntityDetach(() =>
        {
            this.context.Books.RemoveAll();
        });
    }
}