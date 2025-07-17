using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookstoreApiTests.Models;
using BookstoreApiTests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BookstoreApiTests.Tests
{
    [TestFixture]
    public class AuthorTests : BaseTest
    {
        [Test]
        public async Task CreateAuthor_ShouldReturnCreatedData()
        {
            var author = TestDataFactory.GenerateAuthor();

            var createResp = await _authorClient.CreateAuthorAsync(author);

            createResp.IsSuccessful.Should().BeTrue();
            createResp.StatusCode.Should().Be(HttpStatusCode.OK,
                $"Author creation should return 200 OK, but got {createResp.StatusCode}");
            createResp.Data.FirstName.Should().Be(author.FirstName,
                $"Author’s first name should match input: {author.FirstName}");
            createResp.Data.LastName.Should().Be(author.LastName,
                $"Author’s last name should match input: {author.LastName}");
        }

        [Test]
        public async Task GetAuthor_WithInvalidId_ShouldReturnNotFound()
        {
            var response = await _authorClient.GetAuthorByIdAsync(-9999);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound,
                $"Getting author with invalid ID should return 404 Not Found, but got {response.StatusCode}");
        }

        [Test]
        public async Task UpdateAuthor_ShouldReturnSuccess()
        {
            var author = TestDataFactory.GenerateAuthor();
            var createResp = await _authorClient.CreateAuthorAsync(author);

            createResp.IsSuccessful.Should().BeTrue("Author was not created succesfully");
            var createdId = createResp.Data.Id;

            var updatedAuthor = TestDataFactory.GenerateAuthor(id:createdId);

            var updateResp = await _authorClient.UpdateAuthorAsync(createdId, updatedAuthor);


            updateResp.IsSuccessful.Should().BeTrue("updating an author was not successfull");
            updateResp.StatusCode.Should().Be(HttpStatusCode.OK);
            updateResp.Data.FirstName.Should().Be(updatedAuthor.FirstName,
                $"Author’s first name should match input: {updatedAuthor.FirstName}");
            updateResp.Data.LastName.Should().Be(updatedAuthor.LastName,
                $"Author’s last name should match input: {updatedAuthor.LastName}");
        }

        [Test]
        public async Task DeleteAuthor_ShouldReturnSuccess()
        {
            var author = TestDataFactory.GenerateAuthor();
            var createResp = await _authorClient.CreateAuthorAsync(author);

            createResp.IsSuccessful.Should().BeTrue("Author was not created succesfully");
            var authorId = createResp.Data.Id;

            var deleteResp = await _authorClient.DeleteAuthorAsync(authorId);

            deleteResp.IsSuccessful.Should().BeTrue("deleting an existing author was not successfull");

            var getResp = await _authorClient.GetAuthorByIdAsync(authorId);

            getResp.StatusCode.Should().Be(HttpStatusCode.NotFound,
                $"Getting deleted author should return 404 Not Found, but got {getResp.StatusCode}");
        }

        [Test]
        public async Task GetAllAuthors_ShouldContainKnownAuthor()
        {
            var response = await _authorClient.GetAllAuthorsAsync();

            response.IsSuccessful.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Data.Should().NotBeNullOrEmpty("API returned a empty list of authors");

            var knownAuthor = response.Data.FirstOrDefault(a => a.Id == 1);
            knownAuthor.Should().NotBeNull("The author with ID=1 is not present in the list");

            knownAuthor.FirstName.Should().Be("First Name 1");
            knownAuthor.LastName.Should().Be("Last Name 1");
        }

    }
}
