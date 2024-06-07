using Application.Authors.Models;
using Application.Books.Model;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Author, GetAuthorModel>
                .NewConfig()
                .Map(dest => dest.Books, src => src.AuthorBooks.Adapt<IEnumerable<GetBookModel>>());

            TypeAdapterConfig<Book, GetBookModel>
                .NewConfig()
                .Map(dest => dest.Authors, src => src.AuthorBooks.Adapt<IEnumerable<GetAuthorModel>>());
        }
    }
}
