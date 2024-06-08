using Application.Authors.Models;
using Application.Books.Model;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<AuthorBooks, BookModel>
                .NewConfig()
                .Map(x => x, x => x.Book)
                .Map(x => x.Id, x => x.Book.Id);

            TypeAdapterConfig<AuthorBooks, AuthorModel>
                .NewConfig()
                .Map(x => x, x => x.Author)
                .Map(x => x.Id, x => x.Author.Id);

            TypeAdapterConfig<Author, AuthorModel>
                .NewConfig()
                .Map(x => x.Books, x => x.AuthorBooks.Adapt<IEnumerable<BookModel>>());

            TypeAdapterConfig<Book, BookModel>
                .NewConfig()
                .Map(x => x.Authors, x => x.AuthorBooks.Adapt<IEnumerable<AuthorModel>>());
        }
    }
}
