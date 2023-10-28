using LibraryAPI_Mauricio.DAL;
using LibraryAPI_Mauricio.DAL.Entities;
using LibraryAPI_Mauricio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace LibraryAPI_Mauricio.Domain.Services
{
    public class BookService : IBookService
    {
        public readonly DataBaseContext _context;

        public BookService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();// TRAER TODOS LOS DATOS DE MI TABLA BOOKS
            
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            try
            {

                book.Id = Guid.NewGuid(); // ASI SE ASIGNA UN ID AUTOMATICAMENTE A UN NUEVO REGISTRO.
                book.CreatedDate = DateTime.Now;

                _context.Books.Add(book); // AQUI ESTOY CRENADO EL OBJETO BOOK EN EL CONTEXTO DE MI BD
                await _context.SaveChangesAsync(); // AQUI YA ESTOY YENDO A LA BD PARA HACER EL INSERT EN LA TABLA BOOKS

                return book;

            }
            catch (DbUpdateException dbUpdateException)
            {
                // ESTA EXCEPTION ME CAPTURA UN MENSAJE CUANDO EL LIBRO YA EXISTE (DUPLICADOS)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
                // COALLESENSES NOTATION: ??
            }
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> GetBookByNameAsync(string name)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Name == name);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            try
            {

                book.ModifiedDate = DateTime.Now;

                _context.Books.Update(book); 
                await _context.SaveChangesAsync(); 

                return book;

            }
            catch (DbUpdateException dbUpdateException)
            {
                
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
                
            }
        }

        public async Task<Book> DeleteBookAsync(Guid id)
        {
            try
            {

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return null;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);

            }
        }
    }
}
