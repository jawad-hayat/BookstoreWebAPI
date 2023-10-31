namespace BookstoreWebAPI.Dtos
{
    public class OrderItemDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}