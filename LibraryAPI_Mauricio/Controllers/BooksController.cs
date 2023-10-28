using LibraryAPI_Mauricio.DAL.Entities;
using LibraryAPI_Mauricio.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_Mauricio.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // ESTA ES LA PRIMERA PARTE DE LA URL DE ESTA API: URL = API/BOOKS
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /* EN UN CONTROLADOR LOS METODOS CAMBIAN DE NOMBRE, Y REALMENTE SE LLAMAN ACCIONES (ACTIONS).
         * SI ES UNA API, SE DENOMINA ENDPOINT.
         * TODO ENDPOINT RETORNA UN ACTIONRESULT, SIGNIFICA QUE RETORNA EL RESULTADO DE UNA ACCION.
        */

        [HttpGet, ActionName("Get")]
        [Route("GetAll")] // AQUI CONCATENO LA URL INICIAL: URL = API/BOOKS/GET

        public async Task<ActionResult<IEnumerable<Book>>> GetBooksAsync()
        {
            var books = await _bookService.GetBooksAsync(); // AQUI ESTOY YENDO A MI CAPA DOMAIN PARA TRAERME LA LISTA DE LIBROS

            if(books == null || !books.Any()) // EL METODO ANY SIGNIFICA DE QUE AL MENOS HAY UN ELEMENTO.
                                              // EL METODO !ANY SIGNIFICA DE QUE NO HAY NINGUN ELEMENTO.
            {
                return NotFound(); // 404
            }

            return Ok(books); // 200
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateBookAsync(Book book)
        {
            try
            {
                var createdBook = await _bookService.CreateBookAsync(book);

                if(createdBook == null)
                {
                    return NotFound();
                }
                return Ok(createdBook);
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El libro {0} ya existe.", book.Name));

                }

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]

        public async Task<ActionResult<IEnumerable<Book>>> GetBookByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("¡ID es requerido!");

            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                                               
            {
                return NotFound(); 
            }

            return Ok(book);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetByName/{name}")]

        public async Task<ActionResult<IEnumerable<Book>>> GetBookByNameAsync(string name)
        {
            if (name == null) return BadRequest("¡NOMBRE del libro es requerido!");

            var book = await _bookService.GetBookByNameAsync(name);

            if (book == null)

            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPut, ActionName("Update")]
        [Route("Update")]
        public async Task<ActionResult<Book>> UpdateBookAsync(Book book)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(book);             
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("{0} ya existe.", book.Name));

                }

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Book>> DeleteBookAsync(Guid id)
        {
            
            if (id == null) return BadRequest("¡ID es requerido!");
            var deletedBook = await _bookService.DeleteBookAsync(id);
            if (deletedBook == null) return NotFound("Libro no encontrado");
            return Ok(deletedBook);

        }

    }
}
