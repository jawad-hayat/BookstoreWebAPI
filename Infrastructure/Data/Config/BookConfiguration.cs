using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
            builder.Property(e => e.ImageUrl).IsRequired();
            builder.HasOne(e => e.BookType).WithMany().HasForeignKey(e => e.BookTypeId);
            builder.HasOne(e => e.BookBrand).WithMany().HasForeignKey(e => e.BookBrandId);
        }
    }
}
