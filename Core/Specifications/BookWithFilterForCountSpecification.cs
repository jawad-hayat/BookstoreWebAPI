using Core.Entities;
using Core.Specifications;

namespace Core.Specification
{
    public class BookWithFilterForCountSpecification : BaseSpecipication<Book>
    {
        public BookWithFilterForCountSpecification(BookSpecParams bookParams) 
            : base(x =>
            (String.IsNullOrEmpty(bookParams.Search) || x.Name.ToLower().Contains(bookParams.Search)) &&
            (!bookParams.BrandId.HasValue || x.BookBrandId == bookParams.BrandId) &&
            (!bookParams.TypeId.HasValue || x.BookTypeId == bookParams.TypeId)
        )
        {
            
        }
    }
}
