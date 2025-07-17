using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using BookstoreApiTests.Models;

namespace BookstoreApiTests.Clients
{
    public class AuthorClient
    {
        private readonly RestClient _client;

        public AuthorClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse<Author>> CreateAuthorAsync(Author author)
        {
            var request = new RestRequest("Authors", Method.Post).AddJsonBody(author);
            return await _client.ExecuteAsync<Author>(request);
        }

        public async Task<RestResponse<Author>> GetAuthorByIdAsync(int id)
        {
            var request = new RestRequest($"Authors/{id}", Method.Get);
            return await _client.ExecuteAsync<Author>(request);
        }

        public async Task<RestResponse<List<Author>>> GetAllAuthorsAsync()
        {
            var request = new RestRequest("Authors", Method.Get);
            return await _client.ExecuteAsync<List<Author>>(request);
        }

        public async Task<RestResponse<Author>> UpdateAuthorAsync(int id, Author updatedAuthor)
        {
            var request = new RestRequest($"Authors/{id}", Method.Put).AddJsonBody(updatedAuthor);
            return await _client.ExecuteAsync<Author>(request);
        }

        public async Task<RestResponse> DeleteAuthorAsync(int id)
        {
            var request = new RestRequest($"Authors/{id}", Method.Delete);
            return await _client.ExecuteAsync<Author>(request);
        }
    }
}
