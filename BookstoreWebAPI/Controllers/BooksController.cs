using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public BooksController(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<List<Books>>> GetBooks()
        {
            var books = await _bookRepo.GetBooksAsync();
            return Ok(books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(int id)
        {
            return await _bookRepo.GetBookByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BookBrand>>> GetBrands()
        {
            return Ok(await _bookRepo.GetBooksBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<BookType>>> GetTypes()
        {
            return Ok(await _bookRepo.GetBooksTypesAsync());
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
