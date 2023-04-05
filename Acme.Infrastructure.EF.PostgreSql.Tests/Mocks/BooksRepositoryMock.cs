using Acme.Core.Books;
using Acme.Core.Books.Interfaces;
using Acme.Core.Paging.Interfaces;
using Moq;

namespace Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;

/// <summary>
/// Books repository mock class.
/// </summary>
/// <seealso cref="IBooksRepository"/>
public class BooksRepositoryMock : Mock<IBooksRepository>
{
    /// <summary>
    /// Verifies that the <see cref="IBooksRepository.GetAllPaged"/> repository method was called specified number of times.
    /// </summary>
    /// <param name="pagedRequest">Paged request the method should have been called with.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksRepositoryMock"/> instance.</returns>
    public BooksRepositoryMock VerifyGetAllPaged(IPagedRequest pagedRequest, Times times)
    {
        this.Verify(x => x.GetAllPaged(pagedRequest), times);

        return this;
    }

    /// <summary>
    /// Setups the <see cref="IBooksRepository.GetByIdAsync"/> repository method.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="BooksRepositoryMock"/> instance.</returns>
    public BooksRepositoryMock SetupGetByIdAsync(string id, IBook? result)
    {
        this.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksRepository.GetByIdAsync"/> repository method was called specified number of times for the given identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksRepositoryMock"/> instance.</returns>
    public BooksRepositoryMock VerifyGetByIdAsync(string id, Times times)
    {
        this.Verify(x => x.GetByIdAsync(id), times);

        return this;
    }

    /// <summary>
    /// Setups the <see cref="IBooksRepository.CreateAsync"/> repository method.
    /// </summary>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="BooksRepositoryMock"/> instance.</returns>
    public BooksRepositoryMock SetupCreateAsync(IBook result)
    {
        this.Setup(x => x.CreateAsync(It.IsAny<IBookCreate>())).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksRepository.CreateAsync"/> repository method was called specified number of times with the given model.
    /// </summary>
    /// <param name="createModel">Explicit create model.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksRepositoryMock"/> instance.</returns>
    public BooksRepositoryMock VerifyCreateAsync(BookCreate createModel, Times times)
    {
        this.Verify(x => x.CreateAsync(createModel), times);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksRepository.UpdateAsync"/> repository method was called specified number of times with the given model.
    /// </summary>
    /// <param name="updateModel">Explicit update model.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksRepositoryMock"/> instance.</returns>
    public BooksRepositoryMock VerifyUpdateAsync(BookUpdate updateModel, Times times)
    {
        this.Verify(x => x.UpdateAsync(updateModel), times);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IBooksRepository.DeleteAsync"/> repository method was called specified number of times with the given model.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="BooksRepositoryMock"/> instance.</returns>
    public BooksRepositoryMock VerifyDeleteAsync(string id, Times times)
    {
        this.Verify(x => x.DeleteAsync(id), times);

        return this;
    }
}