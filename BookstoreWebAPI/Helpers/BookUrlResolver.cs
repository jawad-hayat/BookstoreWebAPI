using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebAPI.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace BookstoreWebAPI.Helpers
{
    public class BookUrlResolver : IValueResolver<Book, BookToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public BookUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Book source, BookToReturnDto destination, string destMember,
            ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"] + source.ImageUrl;
            }

            return null;
        }
    }
}
