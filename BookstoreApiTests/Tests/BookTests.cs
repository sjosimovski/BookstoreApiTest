using System.Net;
using System.Threading.Tasks;
using BookstoreApiTests.Models;
using BookstoreApiTests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BookstoreApiTests.Tests
{
    [TestFixture]
    public class BookTests : BaseTest
    {
        [Test]
        public async Task CreateBook_ShouldReturnCreatedData()
        {
            var book = TestDataFactory.GenerateBook();

            var createResp = await _bookClient.CreateBookAsync(book);

            createResp.IsSuccessful.Should().BeTrue();
            createResp.StatusCode.Should().Be(HttpStatusCode.OK,
                $"Book creation should return 200 OK, but got {createResp.StatusCode}");
            createResp.Data.Title.Should().Be(book.Title,
                $"Book title should match input: {book.Title}");
            createResp.Data.PageCount.Should().Be(book.PageCount,
                $"Book page count should match input: {book.PageCount}");
            createResp.Data.PublishDate.Should().Be(book.PublishDate,
                $"Book publish date should match input: {book.PublishDate}");
        }

        [Test]
        public async Task GetBook_WithInvalidId_ShouldReturnNotFound()
        {
            var response = await _bookClient.GetBookByIdAsync(-9999);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound,
                $"Getting book with invalid ID should return 404 Not Found, but got {response.StatusCode}");
        }

        [Test]
        public async Task UpdateBook_ShouldReturnSuccess()
        {
            var book = TestDataFactory.GenerateBook();
            var createResp = await _bookClient.CreateBookAsync(book);

            createResp.IsSuccessful.Should().BeTrue("Book was not created successfully");
            var createdId = createResp.Data.Id;

            var updatedBook = TestDataFactory.GenerateBook(id: createdId);

            var updateResp = await _bookClient.UpdateBookAsync(createdId, updatedBook);

            updateResp.IsSuccessful.Should().BeTrue("Updating the book was not successful");
            updateResp.StatusCode.Should().Be(HttpStatusCode.OK,
                $"Book update should return 200 OK, but got {updateResp.StatusCode}");
            updateResp.Data.Title.Should().Be(updatedBook.Title,
                $"Book title should match updated input: {updatedBook.Title}");
            updateResp.Data.PageCount.Should().Be(updatedBook.PageCount,
                $"Book page count should match updated input: {updatedBook.PageCount}");
            updateResp.Data.PublishDate.Should().Be(updatedBook.PublishDate,
                $"Book publish date should match updated input: {updatedBook.PublishDate}");
        }

        [Test]
        public async Task DeleteBook_ShouldReturnSuccess()
        {
            var book = TestDataFactory.GenerateBook();
            var createResp = await _bookClient.CreateBookAsync(book);

            createResp.IsSuccessful.Should().BeTrue("Book was not created successfully");
            var bookId = createResp.Data.Id;

            var deleteResp = await _bookClient.DeleteBookAsync(bookId);

            deleteResp.IsSuccessful.Should().BeTrue("Deleting an existing book was not successful");

            var getResp = await _bookClient.GetBookByIdAsync(bookId);

            getResp.StatusCode.Should().Be(HttpStatusCode.NotFound,
                $"Getting deleted book should return 404 Not Found, but got {getResp.StatusCode}");
        }

        [Test]
        public async Task GetAllBooks_ShouldContainKnownBook()
        {
            var response = await _bookClient.GetAllBooksAsync();

            response.IsSuccessful.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Data.Should().NotBeNullOrEmpty("API returned a empty list of books");

            var knownBook = response.Data.FirstOrDefault(b => b.Id == 1);
            knownBook.Should().NotBeNull("The book with ID=1 is not present in the list");

            knownBook.Title.Should().Be("Book 1");
            knownBook.PageCount.Should().Be(100);
        }
    }
}
