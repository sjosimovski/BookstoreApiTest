using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using BookstoreApiTests.Models;

namespace BookstoreApiTests.Clients
{
    public class BookClient
    {
        private readonly RestClient _client;

        public BookClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse<Book>> CreateBookAsync(Book book)
        {
            var request = new RestRequest("Books", Method.Post).AddJsonBody(book);
            return await _client.ExecuteAsync<Book>(request);
        }

        public async Task<RestResponse<Book>> GetBookByIdAsync(int id)
        {
            var request = new RestRequest($"Books/{id}", Method.Get);
            return await _client.ExecuteAsync<Book>(request);
        }

        public async Task<RestResponse<List<Book>>> GetAllBooksAsync()
        {
            var request = new RestRequest("Books", Method.Get);
            return await _client.ExecuteAsync<List<Book>>(request);
        }

        public async Task<RestResponse<Book>> UpdateBookAsync(int id, Book updatedBook)
        {
            var request = new RestRequest($"Books/{id}", Method.Put).AddJsonBody(updatedBook);
            return await _client.ExecuteAsync<Book>(request);
        }

        public async Task<RestResponse> DeleteBookAsync(int id)
        {
            var request = new RestRequest($"Books/{id}", Method.Delete);
            return await _client.ExecuteAsync(request);
        }
    }
}
