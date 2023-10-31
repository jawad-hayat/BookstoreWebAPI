namespace Core.Entities.OrderAggregate
{
    public class BookItemOrdered
    {
        public BookItemOrdered()
        {
        }

        public BookItemOrdered(int bookItemId, string bookName, string pictureUrl)
        {
            BookItemId = bookItemId;
            BookName = bookName;
            PictureUrl = pictureUrl;
        }

        public int BookItemId { get; set; }
        public string BookName { get; set; }
        public string PictureUrl { get; set; }
    }
}