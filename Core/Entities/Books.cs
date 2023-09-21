namespace Core.Entities
{
    public class Books : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }

        public BookType BookType { get; set; }

        public int BookTypeId { get; set; }
        public BookBrand BookBrand { get; set; }

        public int BookBrandId { get; set; }

    }
}
