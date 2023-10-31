using Core.Entities;

namespace Core.Specifications
{
    public class BookWithTypesAndBrandsSpecification : BaseSpecipication<Book>
    {
        //public BookWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId) :base(x=> 
        //(!brandId.HasValue || x.BookBrandId == brandId) && (!typeId.HasValue || x.BookTypeId == typeId))
        //{
        //    AddInclude(x => x.BookType);
        //    AddInclude(x => x.BookBrand);

        //    if (!string.IsNullOrEmpty(sort))
        //    {
        //        switch (sort)
        //        {
        //            case "priceAsc": 
        //                AddOrderBy(x => x.Price); break;
        //            case "priceDesc":
        //                AddOrderByDescending(x => x.Price); break;
        //            case "nameAsc":
        //                AddOrderBy(x => x.Name); break;
        //            case "nameDesc":
        //                AddOrderByDescending(x => x.Name); break;

        //        }
        //    }
        //}

        public BookWithTypesAndBrandsSpecification(BookSpecParams bookParams) : base(x =>
            (String.IsNullOrEmpty(bookParams.Search) || x.Name.ToLower().Contains(bookParams.Search)) &&
            (!bookParams.BrandId.HasValue || x.BookBrandId == bookParams.BrandId) &&
            (!bookParams.TypeId.HasValue || x.BookTypeId == bookParams.TypeId)
            )
        {
            AddInclude(x => x.BookType);
            AddInclude(x => x.BookBrand);
            ApplyPaging(bookParams.PageSize * (bookParams.PageIndex - 1), bookParams.PageSize);

            if (!string.IsNullOrEmpty(bookParams.Sort))
            {
                switch (bookParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public BookWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.BookType);
            AddInclude(x => x.BookBrand);
        }
    }
}
