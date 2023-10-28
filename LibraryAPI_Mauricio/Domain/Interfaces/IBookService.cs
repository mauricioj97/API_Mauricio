using LibraryAPI_Mauricio.DAL.Entities;

namespace LibraryAPI_Mauricio.Domain.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync(); // UNA FIRMA DE METODO
        Task<Book> CreateBookAsync(Book book);
        Task<Book> GetBookByIdAsync(Guid id);
        Task<Book> GetBookByNameAsync(String name);
        Task<Book> UpdateBookAsync(Book book);
        Task<Book> DeleteBookAsync(Guid id);
    }
}
