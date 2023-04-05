using Acme.Core.Books;
using Acme.Core.Books.Interfaces;
using Moq;

namespace Acme.Application.Tests.Mocks;

/// <summary>
/// Books service mock class.
/// </summary>
/// <seealso cref="IBooksService"/>
public class BooksServiceMock : Mock<IBooksService>
{
    /// <summary>
    /// Setups the <see cref="IBooksService.GetByIdAsync"/> service method.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="BooksServiceMock"/> instance.</returns>
    public BooksServiceMock SetupGetByIdAsync(string id, IBook? result)
    {
        this.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksService.GetByIdAsync"/> service method was called specified number of times.
    /// </summary>
    /// <param name="id">Explicit identifier.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksServiceMock"/> instance.</returns>
    public BooksServiceMock VerifyGetByIdAsync(string id, Times times)
    {
        this.Verify(x => x.GetByIdAsync(id), times);

        return this;
    }

    /// <summary>
    /// Setups the <see cref="IBooksService.CreateAsync"/> service method.
    /// </summary>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="BooksServiceMock"/> instance.</returns>
    public BooksServiceMock SetupCreateAsync(IBook result)
    {
        this.Setup(x => x.CreateAsync(It.IsAny<IBookCreate>())).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksService.CreateAsync"/> service method was called specified number of times with the given model.
    /// </summary>
    /// <param name="name">Explicit name.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksServiceMock"/> instance.</returns>
    public BooksServiceMock VerifyCreateAsync(string name, Times times)
    {
        this.Verify(x => x.CreateAsync(It.Is<BookCreate>(bc => bc.Name == name)), times);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksService.UpdateAsync"/> service method was called specified number of times with the given model.
    /// </summary>
    /// <param name="id">Explicit identifier.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksServiceMock"/> instance.</returns>
    public BooksServiceMock VerifyUpdateAsync(string id, Times times)
    {
        this.Verify(x => x.UpdateAsync(It.Is<BookUpdate>(bu => bu.Id == id)), times);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksService.DeleteAsync"/> service method was called specified number of times with the given model.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksServiceMock"/> instance.</returns>
    public BooksServiceMock VerifyDeleteAsync(string id, Times times)
    {
        this.Verify(x => x.DeleteAsync(id), times);

        return this;
    }
}