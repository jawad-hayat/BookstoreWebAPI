using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Books> GetBookByIdAsync(int id)
        {
            return await _db.Books
                .Include(b => b.BookBrand)
                .Include(b => b.BookType)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IReadOnlyList<Books>> GetBooksAsync()
        {
            return await _db.Books
                .Include(b => b.BookBrand)
                .Include(b => b.BookType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<BookBrand>> GetBooksBrandsAsync()
        {
            return await _db.BooksBrand.ToListAsync();
        }

        public async Task<IReadOnlyList<BookType>> GetBooksTypesAsync()
        {
            return await _db.BooksType.ToListAsync();
        }
    }
}
