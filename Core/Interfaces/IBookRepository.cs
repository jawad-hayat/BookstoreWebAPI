
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IReadOnlyList<Books>> GetBooksAsync();
        Task<IReadOnlyList<BookBrand>> GetBooksBrandsAsync();
        Task<IReadOnlyList<BookType>> GetBooksTypesAsync();
        Task<Books> GetBookByIdAsync(int id);
    }
}
