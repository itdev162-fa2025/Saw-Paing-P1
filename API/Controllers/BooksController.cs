using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_context.Books.ToList());
        }

        
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            var success = _context.SaveChanges() > 0;

            if (success)
            {
                return CreatedAtAction(nameof(AddBook), new { id = book.Id }, book);
            }

            return BadRequest("Error adding the book.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book); 
            var success = _context.SaveChanges() > 0; 

            if (success)
            {
                return NoContent(); 
            }

            return BadRequest("Error deleting the book."); 
        }
    }
}