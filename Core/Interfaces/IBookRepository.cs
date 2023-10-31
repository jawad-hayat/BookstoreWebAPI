
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task<IReadOnlyList<BookBrand>> GetBooksBrandsAsync();
        Task<IReadOnlyList<BookType>> GetBooksTypesAsync();
        Task<Book> GetBookByIdAsync(int id);
    }
}
