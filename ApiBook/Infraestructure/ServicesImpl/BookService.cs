using DTOs;
using Helpers;
using Models;
using System.Net.Http.Json;

namespace Services.ServicesImpl
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync(ApiEndpoints.BOOKS);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.BOOK_BY_ID, id));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task CreateBookAsync(BookCreateDto newBook)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiEndpoints.BOOKS, newBook);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateBookAsync(int id, BookUpdateDto updatedBook)
        {
            var response = await _httpClient.PutAsJsonAsync(string.Format(ApiEndpoints.BOOK_BY_ID, id), updatedBook);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(string.Format(ApiEndpoints.BOOK_BY_ID, id));

            response.EnsureSuccessStatusCode();
        }
    }
}
