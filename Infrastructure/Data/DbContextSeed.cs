
using Core.Entities;
using Core.Entities.OrderAggregate;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class DbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (!db.BooksBrand.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonConvert.DeserializeObject<List<BookBrand>>(brandsData);
                db.BooksBrand.AddRange(brands);
                await db.SaveChangesAsync();
            }

            if (!db.BooksType.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonConvert.DeserializeObject<List<BookType>>(typesData);
                db.BooksType.AddRange(types);
                await db.SaveChangesAsync();
            }

            if (!db.Books.Any())
            {
                var booksData = File.ReadAllText("../Infrastructure/Data/SeedData/books.json");
                var books = JsonConvert.DeserializeObject<List<Book>>(booksData);
                db.Books.AddRange(books);
                await db.SaveChangesAsync();
            }
            if (!db.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var methods = JsonConvert.DeserializeObject<List<DeliveryMethod>>(deliveryData);
                db.DeliveryMethods.AddRange(methods);
                await db.SaveChangesAsync();
            }
            //just for debugging purpose
            //if (!db.Books.Any())
            //{
            //    var booksData = new List<Books> {
            //    new Books{
            //        Name =  "Tafseer ibn Kaseer",
            //        Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
            //        Price = 150,
            //        ImageUrl = "images/products/sb-ang2.png",
            //        BookTypeId = 1,
            //        BookBrandId = 1},
            //    new Books{
            //        Name =  "asdasr",
            //        Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
            //        Price = 150,
            //        ImageUrl = "images/products/sb-an2.png",
            //        BookTypeId = 1,
            //        BookBrandId = 1}
            //    };

            //    db.Books.AddRange(booksData);
            //    await db.SaveChangesAsync();
            //}
        }
    }
    }
