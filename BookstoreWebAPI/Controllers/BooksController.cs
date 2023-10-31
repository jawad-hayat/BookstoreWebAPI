using AutoMapper;
using BookstoreWebAPI.Dtos;
using BookstoreWebAPI.Errors;
using BookstoreWebAPI.Helpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseApiController
    {
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IGenericRepository<BookBrand> _brandRepo;
        private readonly IGenericRepository<BookType> _typeRepo;
        private readonly IMapper _mapper;

        public BooksController(
            IGenericRepository<Book> bookRepo,
            IGenericRepository<BookBrand> brandRepo,
            IGenericRepository<BookType> typeRepo,
            IMapper mapper
            )
        {
            _bookRepo = bookRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<Pagination<BookToReturnDto>>> GetBooks([FromQuery] BookSpecParams bookParams)
        {

            var spec = new BookWithTypesAndBrandsSpecification(bookParams);
            
            var countSpec = new BookWithFilterForCountSpecification(bookParams);

            var totalItems = await _bookRepo.CountAsync(countSpec);

            var books = await _bookRepo.ListAsync(spec);
            var data = _mapper
                .Map<IReadOnlyList<Book>, IReadOnlyList<BookToReturnDto>>(books);

            return Ok(new Pagination<BookToReturnDto>(bookParams.PageIndex, bookParams.PageSize, totalItems, data));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) ,StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookToReturnDto>> GetBook(int id)
        {
            var spec = new BookWithTypesAndBrandsSpecification(id);
            var book = await _bookRepo.GetEntityWithSpec(spec);

            if (book == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Book, BookToReturnDto>(book);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BookBrand>>> GetBrands()
        {
            return Ok(await _brandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<BookType>>> GetTypes()
        {
            return Ok(await _typeRepo.ListAllAsync());
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
