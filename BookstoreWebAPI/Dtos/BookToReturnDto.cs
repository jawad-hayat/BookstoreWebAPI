using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Core.Entities;

namespace BookstoreWebAPI.Dtos
{
    public class BookToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string BookType { get; set; }
        public string BookBrand { get; set; }
    }
}

