using DTOs;
using Models;

namespace Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task CreateBookAsync(BookCreateDto newBook);
        Task UpdateBookAsync(int id, BookUpdateDto updatedBook);
        Task DeleteBookAsync(int id);
    }
}
